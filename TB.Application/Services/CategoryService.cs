using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Application.Interfaces;
using TB.Domain.Models;

namespace TB.Application.Services
{
    public class CategoryService:ICategoryService
    {
        private IAppDbContext _context;
        public CategoryService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Category category)
        {
            _context.Categories.Add(category);

            try
            {
                await _context.SaveChangesAsync();
                if (category.Id > 0)
                    return true;

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> Update(Category category)
        {
            _context.Categories.Update(category);

            try
            {
                await _context.SaveChangesAsync();
                if (category.Id > 0)
                    return true;

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            //Category model = new Category
            //{
            //    Id = id
            //};
            Category model = _context.Categories.FirstOrDefault(p => p.Id == id);
            if (model != null)
            {
                _context.Categories.Remove(model);

                try
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            throw new Exception("Not Found Category");
        }
        public async Task<List<Category>> GetAll()
        {
            try
            {
                List<Category> result = _context.Categories.ToList();

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
           
        }
        public async Task<Category> FindById(int id)
        {
            try
            {
                Category result = _context.Categories.FirstOrDefault(p => p.Id == id);

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
           
        }
    }
}
