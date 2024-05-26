using Demo.Application.Model;
using Demo.Domain.Common;
using Demo.Domain.DataObejct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Interface
{
    public interface ICommentService
    {
        Task<bool> CreateAsync(CommentModel model);
        Task<bool> Delete(int id);
        Task<DataComment<CommentModel>> GetPagingAsync(BaseFilter filter);
        Task<bool> UpdateAsync(int id, CommentModel model);
    }
}
