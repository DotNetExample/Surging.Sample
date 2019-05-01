using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Debug.Test1.IApplication.Demo.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surging.Debug.Test1.IApplication.Demo
{
    [ServiceBundle("v1/api/debug/demo/{service}")]
    public interface IDemoApplication : IServiceKey
    {
        Task<string> GetUserName(QueryUserInput input);

        Task<string> GetUserId(string id);

        Task CreatDemo(DemoInput input);

        Task<string> CreateUser();
    }
}
