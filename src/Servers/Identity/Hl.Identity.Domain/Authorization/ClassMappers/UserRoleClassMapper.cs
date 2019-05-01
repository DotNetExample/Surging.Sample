using DapperExtensions.Mapper;
using Hl.Core.ClassMapper;
using Hl.Identity.Domain.Authorization.Users;

namespace Hl.Identity.Domain.Authorization.ClassMappers
{
    public class UserRoleClassMapper : HlClassMapper<UserRole>
    {
        public UserRoleClassMapper()
        {         
            AutoMap();
        }

    }
}
