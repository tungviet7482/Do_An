using Demo.Domain.Common;
using Demo.Domain.DataObejct;
using Demo.Domain.Entity;
using Demo.Domain.Repository.Base;

namespace Demo.Domain.IRepository
{
    public interface ICommentRepository : IBaseRepository<Comment, long>
    {
        Task<DataComment<Comment>> GetPagingAsync(BaseFilter filter);
    }
}
