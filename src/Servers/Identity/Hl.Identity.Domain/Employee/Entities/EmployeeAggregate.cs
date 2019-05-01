
using Hl.Core.Enums;
using Hl.Identity.Domain.Authorization.Users;
using Hl.Identity.Domain.Employee.Models;
using Surging.Core.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace Hl.Identity.Domain.Employee.Entities
{
    public class EmployeeAggregate : FullAuditedEntity<long>
    {
        public EmployeeAggregate()
        {
            Status = Status.Valid;
            UserInfos = new List<UserInfo>();
        }

        public string UserName { get; set; }

        public string ChineseName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birth { get; set; }

        public string NativePlace { get; set; }

        public string Address { get; set; }

        public string Folk { get; set; }

        public PoliticalStatus PoliticalStatus { get; set; }

        public string GraduateInstitutions { get; set; }

        public string Education { get; set; }

        public string Major { get; set; }

        public string Resume { get; set; }

        public string Memo { get; set; }

        public Status Status { get; set; }

       public ICollection<UserInfo> UserInfos { get; set; }
    }
}
