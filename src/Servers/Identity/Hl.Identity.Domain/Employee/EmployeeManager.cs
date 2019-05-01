using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.Domain.Employee.Entities;
using Surging.Core.Dapper.Manager;
using Surging.Core.Dapper.Repositories;
using System;
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
                userInfo.Password = GetService<IPasswordHelper>().EncryptPassword(userInfo.UserName, "123qwe");
                await GetService<IDapperRepository<UserInfo, long>>().InsertAsync(userInfo, conn, tran);

            }, Connection);

        }
    }
}
