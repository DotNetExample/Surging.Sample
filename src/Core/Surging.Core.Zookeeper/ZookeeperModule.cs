﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Cache;
using Surging.Core.CPlatform.Module;
using Surging.Core.CPlatform.Mqtt;
using Surging.Core.CPlatform.Routing;
using Surging.Core.CPlatform.Runtime.Client;
using Surging.Core.CPlatform.Runtime.Server;
using Surging.Core.CPlatform.Serialization;
using Surging.Core.CPlatform.Support;
using Surging.Core.Zookeeper.Configurations;
using System;

namespace Surging.Core.Zookeeper
{
    public class ZookeeperModule : EnginePartModule
    {
        public override void Initialize(CPlatformContainer serviceProvider)
        {
            base.Initialize(serviceProvider);
        }

        /// <summary>
        /// Inject dependent third-party components
        /// </summary>
        /// <param name="builder"></param>
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            base.RegisterBuilder(builder);
            var configInfo = new ConfigInfo(null);
            UseZooKeeperRouteManager(builder, configInfo)
              .UseZooKeeperCacheManager(builder, configInfo)
                .UseZooKeeperServiceSubscribeManager(builder, configInfo)
                .UseZooKeeperCommandManager(builder, configInfo);
        }

        public ContainerBuilderWrapper UseRouteManager(ContainerBuilderWrapper builder, Func<IServiceProvider, IServiceRouteManager> factory)
        {
            builder.RegisterAdapter(factory).InstancePerLifetimeScope();
            return builder;
        }

        public ContainerBuilderWrapper UseSubscribeManager(ContainerBuilderWrapper builder, Func<IServiceProvider, IServiceSubscribeManager> factory)
        {
            builder.RegisterAdapter(factory).InstancePerLifetimeScope();
            return builder;
        }

        public ContainerBuilderWrapper UseCommandManager(ContainerBuilderWrapper builder, Func<IServiceProvider, IServiceCommandManager> factory)
        {
            builder.RegisterAdapter(factory).InstancePerLifetimeScope();
            return builder;
        }

        public ContainerBuilderWrapper UseCacheManager(ContainerBuilderWrapper builder, Func<IServiceProvider, IServiceCacheManager> factory)
        {
            builder.RegisterAdapter(factory).InstancePerLifetimeScope();
            return builder;
        }

        public ContainerBuilderWrapper UseMqttRouteManager(ContainerBuilderWrapper builder, Func<IServiceProvider, IMqttServiceRouteManager> factory)
        {
            builder.RegisterAdapter(factory).InstancePerLifetimeScope();
            return builder;
        }

        /// <summary>
        /// 设置共享文件路由管理者。
        /// </summary>
        /// <param name="builder">Rpc服务构建者。</param>
        /// <param name="configInfo">ZooKeeper设置信息。</param>
        /// <returns>服务构建者。</returns>
        public ZookeeperModule UseZooKeeperRouteManager(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            UseRouteManager(builder, provider =>
          new ZooKeeperServiceRouteManager(
             GetConfigInfo(configInfo),
           provider.GetRequiredService<ISerializer<byte[]>>(),
             provider.GetRequiredService<ISerializer<string>>(),
             provider.GetRequiredService<IServiceRouteFactory>(),
             provider.GetRequiredService<ILogger<ZooKeeperServiceRouteManager>>()));
            return this;
        }

        public ZookeeperModule UseZooKeeperMqttRouteManager(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            UseMqttRouteManager(builder, provider =>
          {
              var result = new ZooKeeperMqttServiceRouteManager(
                   GetConfigInfo(configInfo),
                 provider.GetRequiredService<ISerializer<byte[]>>(),
                   provider.GetRequiredService<ISerializer<string>>(),
                   provider.GetRequiredService<IMqttServiceFactory>(),
                   provider.GetRequiredService<ILogger<ZooKeeperMqttServiceRouteManager>>());
              return result;
          });
            return this;
        }

        /// <summary>
        /// 设置服务命令管理者。
        /// </summary>
        /// <param name="builder">Rpc服务构建者。</param>
        /// <param name="configInfo">ZooKeeper设置信息。</param>
        /// <returns>服务构建者。</returns>
        public ZookeeperModule UseZooKeeperCommandManager(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            UseCommandManager(builder, provider =>
           {
               var result = new ZookeeperServiceCommandManager(
                   GetConfigInfo(configInfo),
                 provider.GetRequiredService<ISerializer<byte[]>>(),
                   provider.GetRequiredService<ISerializer<string>>(),
                 provider.GetRequiredService<IServiceRouteManager>(),
                   provider.GetRequiredService<IServiceEntryManager>(),
                   provider.GetRequiredService<ILogger<ZookeeperServiceCommandManager>>());
               return result;
           });
            return this;
        }

        public ZookeeperModule UseZooKeeperServiceSubscribeManager(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            UseSubscribeManager(builder, provider =>
            {
                var result = new ZooKeeperServiceSubscribeManager(
                    GetConfigInfo(configInfo),
                  provider.GetRequiredService<ISerializer<byte[]>>(),
                    provider.GetRequiredService<ISerializer<string>>(),
                    provider.GetRequiredService<IServiceSubscriberFactory>(),
                    provider.GetRequiredService<ILogger<ZooKeeperServiceSubscribeManager>>());
                return result;
            });
            return this;
        }

        public ZookeeperModule UseZooKeeperCacheManager(ContainerBuilderWrapper builder, ConfigInfo configInfo)
        {
            UseCacheManager(builder, provider =>
             new ZookeeperServiceCacheManager(
               GetConfigInfo(configInfo),
              provider.GetRequiredService<ISerializer<byte[]>>(),
                provider.GetRequiredService<ISerializer<string>>(),
                provider.GetRequiredService<IServiceCacheFactory>(),
                provider.GetRequiredService<ILogger<ZookeeperServiceCacheManager>>()));
            return this;
        }

        private static ConfigInfo GetConfigInfo(ConfigInfo config)
        {
            ZookeeperOption option = null;
            var section = CPlatform.AppConfig.GetSection("Zookeeper");
            if (section.Exists())
                option = section.Get<ZookeeperOption>();
            else if (AppConfig.Configuration != null)
                option = AppConfig.Configuration.Get<ZookeeperOption>();
            if (option != null)
            {
                var sessionTimeout = config.SessionTimeout.TotalSeconds;
                Double.TryParse(option.SessionTimeout, out sessionTimeout);
                config = new ConfigInfo(
                    option.ConnectionString,
                    TimeSpan.FromSeconds(sessionTimeout),
                    option.RoutePath ?? config.RoutePath,
                    option.SubscriberPath ?? config.SubscriberPath,
                    option.CommandPath ?? config.CommandPath,
                    option.CachePath ?? config.CachePath,
                    option.MqttRoutePath ?? config.MqttRoutePath,
                    option.ChRoot ?? config.ChRoot,
                    option.ReloadOnChange != null ? bool.Parse(option.ReloadOnChange) :
                    config.ReloadOnChange,
                   option.EnableChildrenMonitor != null ? bool.Parse(option.EnableChildrenMonitor) :
                    config.EnableChildrenMonitor
                   );
            }
            return config;
        }
    }
}