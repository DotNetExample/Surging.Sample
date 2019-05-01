using System.Threading.Tasks;
using Hl.Identity.Domain.Employee;
using Hl.Identity.Domain.Employee.Entities;
using Hl.Identity.IApplication.Employee;
using Hl.Identity.IApplication.Employee.Dtos;
using Surging.Core.AutoMapper;
using Surging.Core.ProxyGenerator;


namespace Hl.Identity.Application.Employee
{
    public class EmployeeApplication : ProxyServiceBase, IEmployeeApplication
    {       
        public async Task<string> CreateEmployee(CreateEmployeeInput input)
        {
            var employee = input.MapTo<EmployeeAggregate>();
            await GetService<IEmployeeManager>().CreateEmployee(employee);
            return "新增员工成功";
        }
    }
}
