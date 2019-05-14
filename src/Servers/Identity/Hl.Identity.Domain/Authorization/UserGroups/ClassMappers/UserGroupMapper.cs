using DapperExtensions.Mapper;
namespace Hl.Identity.Domain.Authorization.UserGroups.ClassMappers
{
    public class UserGroupMapper : ClassMapper<UserGroup>
    {
        public UserGroupMapper()
        {
            Table("auth_user_group");
            AutoMap();
        }
    }
}
