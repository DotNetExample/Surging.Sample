using System.Threading.Tasks;
using Hl.Core.Validates;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.Domain.Employee;
using Hl.Identity.Domain.Employee.Entities;
using Hl.Identity.IApplication.Employee;
using Hl.Identity.IApplication.Employee.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.CPlatform.Exceptions;
using Surging.Core.Dapper.Repositories;
using Surging.Core.ProxyGenerator;


namespace Hl.Identity.Application.Employee
{
    public class EmployeeApplication : ProxyServiceBase, IEmployeeApplication
    {       
        public async Task<string> CreateEmployee(CreateEmployeeInput input)
        {
            input.CheckDataAnnotations().CheckValidResult();
            var exsitEployee = await GetService<IDapperRepository<EmployeeAggregate, long>>().FirstOrDefaultAsync(p => p.UserName == input.UserName 
            || p.Email == input.Email 
            || p.Phone == input.Phone);
            if (exsitEployee != null)
            {
                throw new BusinessException("已经存在该员工信息,请检查员工账号信息");
            }

            var exsitUserInfo = await GetService<IDapperRepository<UserInfo, long>>().FirstOrDefaultAsync(p => p.UserName == input.UserName
            || p.Email == input.Email
            || p.Phone == input.Phone);
            if (exsitUserInfo != null)
            {
                throw new BusinessException("已经存在该员工信息,请检查员工账号信息");
            }
            var employee = input.MapTo<EmployeeAggregate>();
            await GetService<IEmployeeManager>().CreateEmployee(employee);
            return "新增员工成功";
        }
    }
}
