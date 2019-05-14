using DapperExtensions.Mapper;
using Hl.Identity.Domain.Authorization.Roles;

namespace Hl.Identity.Domain.Authorization.Users.ClassMappers
{
    public class RoleMapper : ClassMapper<Role>
    {
        public RoleMapper()
        {
            AutoMap();
        }
    }
}
