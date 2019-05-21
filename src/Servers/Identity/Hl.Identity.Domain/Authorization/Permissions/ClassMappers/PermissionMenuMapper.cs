using Hl.Core.ClassMapper;
using System;

namespace Hl.Identity.Domain.Authorization.Permissions.ClassMappers
{
    public class PermissionMenuMapper : HlClassMapper<PermissionMenu>
    {
        public PermissionMenuMapper()
        {
            Table("auth_permission_menu");
            AutoMap();
        }
    }
}
