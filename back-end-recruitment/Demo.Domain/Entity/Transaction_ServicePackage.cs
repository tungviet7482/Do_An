using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Entity
{
    public class Transaction_ServicePackage
    {
        public int TransactionId { get; set; }
        public int ServicePackageId { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual ServicePackage ServicePackage { get; set; }
    }
}
