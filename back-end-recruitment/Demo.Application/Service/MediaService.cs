using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Service.Base;
using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using Microsoft.AspNetCore.Http;

namespace Demo.Application.Service
{
    public class MediaService : BaseService, IMediaService
    {
        private readonly IMediaFileRepository _mediaFileRepository;
        private string[] extensionFiles = { ".pdf" };
        private string[] extensionImages = { ".jpg", ".png", ".jpeg", ".webp" };

        public MediaService(IMapper mapper, IMediaFileRepository mediaFileRepository) : base(mapper)
        {
            _mediaFileRepository = mediaFileRepository;
        }

        public async Task<bool> CreateAsync(MediaFileModel model)
        {
            if (model == null)
            {
                return false;
            }
            try
            {
                if (await _mediaFileRepository.CheckDuplicateName(model.File.FileName))
                {

                }

                //var res = await DownloadFile("", model.File);
                //if (res)
                //{
                //    var entity = _mapper.Map<MediaFile>(model);
                //    entity!.FolderId = entity.Extension == "pdf" ? 0 : 1;
                //    var isSuccess = await _mediaFileRepository.CreateAsync(entity);

                //    return isSuccess;
                //}
            }
            catch
            {

            }
            return false;

        }

        public Task<List<MediaFileModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string?> GetUrlAsync(long id = 0)
        {
            var path = await _mediaFileRepository.GetPathByIdAsync(id);
            return path;
        }

        public Task<bool> UpdateAsync(MediaFileModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<MediaFileModel?> DownloadFile(IFormFile fromFile)
        {
            if (fromFile != null)
            {
                var extension = Path.GetExtension(fromFile.FileName);
                var folder = "";
                if (extensionFiles.Contains(extension))
                {
                    folder = "File";
                }
                else if(extensionImages.Contains(extension)) 
                {
                    folder = "Image";                    
                }
                else
                {
                    return null;
                }

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder, fromFile.FileName);

                using (var file = new FileStream(path, FileMode.Create))
                {
                    await fromFile.CopyToAsync(file);
                }
                var folderId = await _mediaFileRepository.GetFolderIdByName(folder);
                var mediaFile = new MediaFile
                {
                    FileName = fromFile.FileName,
                    Extension = Path.GetExtension(fromFile.FileName),
                    Size = fromFile.Length,
                    FolderId = folderId
                };
                var isSuccessed = await _mediaFileRepository.CreateAsync(mediaFile);
                if (isSuccessed)
                {
                    var model = _mapper.Map<MediaFileModel>(mediaFile);
                    model!.Url = await _mediaFileRepository.GetPathByIdAsync(model.Id);
                    return model;
                }
                return null;
            }
            return null;
        }

        public async Task<bool> DeleteFile(string path)
        {
            //var list = Directory.GetFiles(path);
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }

        public async Task<FileStream> GetFile(string fileName)
        {
            string path = Path.Combine("~/Images", fileName);
            if (File.Exists(path))
            {
                return new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            return null;
        }
    }
}
