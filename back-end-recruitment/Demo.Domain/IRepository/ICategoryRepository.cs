using Demo.Domain.Entity;
using Demo.Domain.Repository.Base;

namespace Demo.Domain.IRepository
{
    public interface ICategoryRepository : IBaseRepository<Category, int>
    {
        Task<List<Category_Job>> GetPagingAsync(int pageIndex, int pageSize, string keyWord);
        Task<int> GetTotalCategories();
        Task<int> GetTotalCategoriesByKeyword(string keyword);
    }
}
