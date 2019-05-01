using AutoMapper;
using Hl.Identity.Domain.Employee.Entities;

namespace Hl.Identity.IApplication.Employee.Dtos
{
    public class EmployeeProfiles : Profile
    {
        public EmployeeProfiles()
        {
            CreateMap<CreateEmployeeInput, EmployeeAggregate>();
        }
    }
}
