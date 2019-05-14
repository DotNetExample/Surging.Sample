using Hl.Core.Enums;
using Surging.Core.Domain.Entities.Auditing;

namespace Hl.Identity.Domain.Authorization.UserGroups
{
    public class UserGroup : FullAuditedEntity<long>
    {
        public UserGroup()
        {
            Status = Status.Valid;
        }

        public string ParentId { get; set; }
        public string GroupName { get; set; }
        public Status Status { get; set; }

    }
}
