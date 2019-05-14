using Hl.Core.Enums;
using Surging.Core.Domain.Entities.Auditing;

namespace Hl.Identity.Domain.Authorization.Users
{
    public class UserInfo : FullAuditedEntity<long>
    {
        public UserInfo()
        {
            //Roles = new List<UserRole>();

            Status = Status.Valid;
            Locked = false;
            LoginFailCount = 0;
        }

        public long EmployeeId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int LoginFailCount { get; set; }

        public bool Locked { get; set; }

        public Status Status { get; set; }

        //public EmployeeAggregate Employee { get; set; }

        //public virtual ICollection<UserRole> Roles { get; set; }
    }
}
