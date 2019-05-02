using Hl.BasicData.Common.SystemConf;
using Hl.Core.ServiceApi;
using Hl.Core.Utils;
using Hl.Identity.Domain.Authorization;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.Domain.Employee.Entities;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.CPlatform.Extensions;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Employee
{
    public class EmployeeManager : ManagerBase, IEmployeeManager
    {

        public async Task CreateEmployee(EmployeeAggregate employee)
        {
           
            await UnitOfWorkAsync(async (conn, tran) =>
            {
                var employeeId = await GetService<IDapperRepository<EmployeeAggregate, long>>().InsertAndGetIdAsync(employee,conn,tran);

                var userInfo = new UserInfo()
                {
                    Email = employee.Email,
                    EmployeeId = employeeId,
                    Phone = employee.Phone,
                    UserName = employee.UserName
                };
                var rpcParams = new Dictionary<string, object>() { { "confName", IdentityConstants.SysConfPwdModeName } };
                var pwdConfig = await GetService<IServiceProxyProvider>().Invoke<GetSystemConfOutput>(rpcParams, ApiConsts.BasicData.GetSysConfApi);
                if (pwdConfig == null)
                {
                    throw new BusinessException("获取用户加密模式失败,请先完成系统初始化");
                }
                var generatePwdMode = ConvertHelper.ParseEnum<GeneratePwdMode>(pwdConfig.ConfigValue);
                var plainPwd = string.Empty;
                if (generatePwdMode == GeneratePwdMode.Fixed)
                {
                    rpcParams = new Dictionary<string, object>() { { "confName", IdentityConstants.SysConfFieldModeName } };
                    var fixedPwdConf = await GetService<IServiceProxyProvider>().Invoke<GetSystemConfOutput>(rpcParams, ApiConsts.BasicData.GetSysConfApi);
                    if (pwdConfig == null)
                    {
                        throw new BusinessException("未配置员工用户默认密码");
                    }
                    plainPwd = fixedPwdConf.ConfigValue;
                }
                else
                {
                    plainPwd = PasswordGenerator.GetRandomPwd(IdentityConstants.RandomLen);
                    // :todo email send pwd
                }
                userInfo.Password = GetService<IPasswordHelper>().EncryptPassword(userInfo.UserName, plainPwd);
                await GetService<IDapperRepository<UserInfo, long>>().InsertAsync(userInfo, conn, tran);

            }, Connection);

        }
    }
}
