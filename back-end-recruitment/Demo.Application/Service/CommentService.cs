using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service.Base;
using Demo.Domain.Common;
using Demo.Domain.DataObejct;
using Demo.Domain.Entity;
using Demo.Domain.filter;
using Demo.Domain.IRepository;
using Domain.Infrastructure.Repository;

namespace Demo.Application.Service
{
    public class CommentService: BaseService, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(IMapper mapper, ICommentRepository commentRepository) : base(mapper)
        {
            _commentRepository = commentRepository;
        }

        public async Task<bool> CreateAsync(CommentModel model)
        {
            var comment = _mapper.Map<Comment>(model);
            comment!.IsPublish = false;
            comment!.CreatedAt = DateTime.Now; 
            var isSuccess = await _commentRepository.CreateAsync(comment);
            if (isSuccess)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return false;
            }
            var isSuccessed = await _commentRepository.DeleteAsync(comment);
            return isSuccessed;
        }

        public Task<List<NewsModel>?> GetAllAsync() => throw new NotImplementedException();

        public async Task<DataComment<CommentModel>> GetPagingAsync(BaseFilter filter)
        {
            var comments = await _commentRepository.GetPagingAsync(filter);
            var commentModels = _mapper.Map<List<CommentModel>>(comments.Items);
            return new DataComment<CommentModel>(commentModels, comments.TotalRecord, comments.StarRating, comments.avgStars);
        }

        public async Task<bool> UpdateAsync(int id, CommentModel model)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return false;
            }
            comment.IsPublish = model.IsPublish;
            var isSuccessed = await _commentRepository.UpdateAsync(comment);

            return isSuccessed;
        }

    }
}
