using Surging.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl.Identity.Domain.Authorization.Menus
{
    public class MenuFunction : AuditedEntity<long>
    {
        public long MenuId { get; set; }
        public long FunctionId { get; set; }
    }
}
