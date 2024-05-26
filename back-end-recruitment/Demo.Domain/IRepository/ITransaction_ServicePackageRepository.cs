using Demo.Domain.Entity;
using Demo.Domain.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.IRepository
{
    public interface ITransaction_ServicePackageRepository: IBaseRepository<Transaction_ServicePackage, int>
    {
    }
}
