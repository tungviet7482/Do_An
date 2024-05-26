using AutoMapper;
using Demo.Application.Interface;
using Demo.Application.Model;
using Demo.Application.Service.Base;
using Demo.Domain.Common;
using Demo.Domain.Entity;
using Demo.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Service
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository) : base(mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> CreateAsync(CategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            category.CreateAt = DateTime.Now;

            var isSuccess = await _categoryRepository.CreateAsync(category);
            if (isSuccess)
            {
                return true;
            }

            return false;
        }


        public async Task<bool> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                return false;
            }    
            var isSuccessed = await _categoryRepository.DeleteAsync(category);
            return isSuccessed;
        }

        public async Task<int> GetTotalCategories()
        {
            return await _categoryRepository.GetTotalCategories();
        }

        public async Task<List<CategoryModel>?> GetAllAsync()
        {
            var categories  = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryModel>>(categories);
        }

        public async Task<DataObject<CategoryModel>> GetPagingAsync(BaseFilter page)
        {
            var total = 0;
            var categories = await _categoryRepository.GetPagingAsync(page.PageIndex, page.PageSize, page.KeyWord);
            var categoriesModel = _mapper.Map<List<CategoryModel>>(categories);
            if (page.KeyWord == null)
            {
                total = await _categoryRepository.GetTotalCategories();
            }
            else
            {
                total = await _categoryRepository.GetTotalCategoriesByKeyword(page.KeyWord);
            }
            return new DataObject<CategoryModel>(categoriesModel, total);
        }

        public async Task<bool> UpdateAsync(int id, CategoryModel model)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                return false;
            }

            category.Name = model.Name;
            category.UpdateAt = DateTime.Now;
            category.DisplayOrder = model.DisplayOrder;

            var isSuccessed = await _categoryRepository.UpdateAsync(category);

            return isSuccessed;
        }
    }
}
