using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using Shop.Data.EntityF;

namespace Domain.Infrastructure.Repository
{
    public class MediaFileRepository : IMediaFileRepository
    {
        private readonly DbRecruitmentContext _db;

        public MediaFileRepository(DbRecruitmentContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(MediaFile entity)
        {
            await _db.MediaFiles.AddAsync(entity);
            var row = await _db.SaveChangesAsync();
            if (row > 0)
            {
                return true ;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(MediaFile entity)
        {
            if (entity == null)
            {
                return false;
            }

            _db.MediaFiles.Remove(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<bool> DeleteRangeAsync(List<MediaFile> entities)
        {
            if (entities == null)
            {
                return false;
            }

            _db.MediaFiles.RemoveRange(entities);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<List<MediaFile>> GetAllAsync()
        {
            return await _db.MediaFiles.AsNoTracking().ToListAsync();
        }

        public async Task<MediaFile?> GetByIdAsync(long id)
        {
            if (id <= 0)
            {
                return null;
            }
            var mediaFile = await _db.MediaFiles.FindAsync(id);

            return mediaFile;
        }

        public async Task<string?> GetPathByIdAsync(long id)
        {
            var f = await (from file in _db.MediaFiles
                           join folder in _db.MediaFolders
                           on file.FolderId equals folder.Id
                           where file.Id == id
                           select new 
                           { 
                               Name = file.FileName, 
                               Folder = folder.Name 
                           }).FirstOrDefaultAsync();

            if (f == null)
            {
                return $"https://localhost:7017/Image/default.png";
            }

            return $"https://localhost:7017/{f.Folder}/{f.Name}";
        }


        public async Task<bool> CheckDuplicateName(string FileName)
        {
            var enity = await _db.MediaFiles.AsNoTracking().Where(x => x.FileName == FileName).SingleOrDefaultAsync();
            return enity == null;
        }

        public async Task<bool> UpdateAsync(MediaFile entity)
        {
            if (entity == null)
            {
                return false;
            }

            _db.MediaFiles.Update(entity);
            var row = await _db.SaveChangesAsync();

            return row > 0;
        }

        public async Task<int> GetFolderIdByName(string name)
        {
            var id = await _db.MediaFolders.Where(x => x.Name == name).Select(x => x.Id).FirstOrDefaultAsync();
            return id;
        }
    }
}
