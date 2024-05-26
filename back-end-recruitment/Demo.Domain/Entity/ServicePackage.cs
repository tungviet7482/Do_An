using Demo.Domain.Common;

namespace Demo.Domain.Entity
{
    public class ServicePackage: BaseEntity
    {
        public string Name { get; set; }
        public string NameSystem { get; set; }
        public decimal? Price { get; set; }
        public int? Duration { get; set; }
        public int Quota { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<Transaction_ServicePackage> Transaction_ServicePackages { get; set; }
    }
}
