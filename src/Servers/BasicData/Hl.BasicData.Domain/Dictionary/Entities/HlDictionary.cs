using Surging.Core.Domain.Entities.Auditing;

namespace Hl.BasicData.Domain
{
    public class HlDictionary : FullAuditedEntity<long>
    {
        public HlDictionary()
        {
            IsSysPreSet = false;
        }

        public string Code { get; set; }

        public string Value { get; set; }

        public long ParentId { get; set; }

        public int Seq { get; set; }

        public string TypeName { get; set; }

        public bool HasChild { get; set; }

        public bool IsSysPreSet { get; set; }
    }
}
