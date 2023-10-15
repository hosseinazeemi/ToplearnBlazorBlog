using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Application.Interfaces;
using TB.Domain.Models;

namespace TB.Application.Services
{
    public class CommentService : ICommentService
    {
        private IAppDbContext _context;
        public CommentService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Comment model)
        {
            _context.Comments.Add(model);

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
        public async Task<bool> Update(Comment model)
        {
            _context.Comments.Update(model);

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
            //Comment model = new Comment
            //{
            //    Id = id
            //};
            Comment model = _context.Comments.FirstOrDefault(p => p.Id == id);
            if (model != null)
            {
                _context.Comments.Remove(model);

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
            throw new Exception("Not Found Comment");
        }
        public async Task<List<Comment>> GetAllComments(int contentId = 0)
        {
            try
            {
                List<Comment> result;
                if (contentId > 0)
                {
                    result = _context.Comments.Where(p => p.ContentId == contentId).ToList();
                }
                else
                {
                    result = _context.Comments.ToList();
                }

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }

        }
        public async Task<Comment> FindById(int id)
        {
            try
            {
                Comment result = _context.Comments.FirstOrDefault(p => p.Id == id);

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }

        }
    }
}
