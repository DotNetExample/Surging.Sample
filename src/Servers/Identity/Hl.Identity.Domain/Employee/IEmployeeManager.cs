using Hl.Identity.Domain.Employee.Entities;
using Surging.Core.CPlatform.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hl.Identity.Domain.Employee
{
    public interface IEmployeeManager : ITransientDependency
    {
        Task CreateEmployee(EmployeeAggregate employee);
    }
}
