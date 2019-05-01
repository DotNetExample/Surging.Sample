using Hl.Core.ClassMapper;
using Hl.Identity.Domain.Employee.Entities;


namespace Hl.Identity.Domain.Employee.ClassMappers
{
    class EmployeeMapper : HlClassMapper<EmployeeAggregate>
    {
        public EmployeeMapper()
        {
            Table("auth_employee");            
            Map(p => p.UserInfos).Ignore();
            AutoMap();
        }
    }
}
