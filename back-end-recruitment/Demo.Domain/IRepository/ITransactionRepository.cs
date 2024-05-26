using Demo.Domain.Entity;
using Demo.Domain.Repository.Base;

namespace Demo.Domain.IRepository
{
    public interface ITransactionRepository : IBaseRepository<Transaction, int>
    {
        Task<List<UserTransaction>> GetPagingAsync(int pageIndex, int pageSize, string keyWord);
        Task<int> GetTotalTransaction();
        Task<int> GetTotalTransactionByKeyword(string keyword);
    }
}
