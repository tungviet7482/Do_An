using Demo.Domain.Entity;
using Demo.Domain.Repository.Base;

namespace Demo.Domain.IRepository
{
    public interface IMediaFileRepository
    {
        Task<bool> CheckDuplicateName(string FileName);
        Task<bool> CreateAsync(MediaFile entity);
        Task<bool> DeleteAsync(MediaFile entity);
        Task<bool> DeleteRangeAsync(List<MediaFile> entities);
        Task<MediaFile?> GetByIdAsync(long id);
        Task<int> GetFolderIdByName(string name);
        Task<string?> GetPathByIdAsync(long id);
        Task<bool> UpdateAsync(MediaFile entity);
    }
}
