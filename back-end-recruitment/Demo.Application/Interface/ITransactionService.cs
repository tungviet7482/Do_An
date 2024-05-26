using Demo.Application.Model;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interface
{
    public interface ITransactionService
    {
        Task<bool> CreateAsync(TransactionModel model);
        Task<bool> Delete(int id);
        Task<List<TransactionModel>?> GetAllAsync();
        Task<DataObject<UserTransactionModel>> GetPagingAsync(BaseFilter page);
        Task<bool> UpdateAsync(int id, TransactionModel model);
    }
}
