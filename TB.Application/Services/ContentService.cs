using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Application.Interfaces;
using TB.Domain.Models;

namespace TB.Application.Services
{
    public class ContentService : IContentService
    {
        private IAppDbContext _context;
        public ContentService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Content model)
        {
            _context.Contents.Add(model);

            try
            {
                await _context.SaveChangesAsync();
                if (model.Id > 0)
                    return true;

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> Update(Content model)
        {
            _context.Contents.Update(model);

            try
            {
                await _context.SaveChangesAsync();
                if (model.Id > 0)
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
            //Content model = new Content
            //{
            //    Id = id
            //};
            Content model = _context.Contents.FirstOrDefault(p => p.Id == id);
            if (model != null)
            {
                _context.Contents.Remove(model);

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
            throw new Exception("Not Found Content");
        }
        public async Task<List<Content>> GetAll()
        {
            try
            {
                List<Content> result = _context.Contents.ToList();

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }

        }
        public async Task<Content> FindById(int id)
        {
            try
            {
                Content result = _context.Contents.Where(p => p.Id == id)
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .FirstOrDefault();

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }

        }
    }
}
