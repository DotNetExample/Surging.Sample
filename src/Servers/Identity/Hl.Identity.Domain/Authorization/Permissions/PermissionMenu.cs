using Surging.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.Permissions
{
    public class PermissionMenu : AuditedEntity<long>
    {
        public long PermissionId { get; set; }
        public long MenuId { get; set; }
    }
}
