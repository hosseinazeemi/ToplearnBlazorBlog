using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Application.Interfaces;
using TB.Domain.Enums;
using TB.Domain.Models;

namespace TB.Application.Services
{
    public class SiteService : ISiteService
    {
        private IAppDbContext _context;
        public SiteService(IAppDbContext context)
        {
            _context = context;
        }
        public List<Content> GetMenuContents(ContentType type)
        {
            try
            {
                var data = _context.Contents
                    .Where(p => p.Type == type && p.Status == StatusType.Active)
                    .OrderByDescending(p => p.Id)
                    .Include(p => p.User)
                    .Select(p => new Content
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Image = p.Image,
                        CreatedAt = p.CreatedAt,
                        User = p.User,
                        UserId = p.UserId

                    }).Take(10).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Category> GetMenuCategories()
        {
            try
            {
                var data = _context.Categories
                    .Where(p => p.Status == StatusType.Active)
                    .OrderByDescending(p => p.Id)
                    .Select(p => new Category
                    {
                        Id = p.Id,
                        Name = p.Name,

                    }).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Content> GetTodayNews()
        {
            try
            {
                List<Content> data = _context.Contents
                    .Where(p => p.Type == ContentType.News && p.Status == StatusType.Active)
                    .Select(p => new Content
                    {
                        Id = p.Id,
                        Title = p.Title
                    })
                    .OrderByDescending(p => p.Id)
                    .Take(10).ToList();

                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Content? GetLastNewsBanner()
        {
            try
            {
                Content? data = _context.Contents
                    .Where(p => p.Status == StatusType.Active && p.Type == ContentType.News)
                    .Include(p => p.User)
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.Id)
                    .Select(p => new Content
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Image = p.Image,
                        CreatedAt = p.CreatedAt,
                        User = p.User,
                        Category = p.Category,
                        UserId = p.UserId,
                        CategoryId = p.CategoryId
                    }).FirstOrDefault();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Content>? GetLastNews()
        {
            try
            {
                List<Content>? data = _context.Contents
                    .Where(p => p.Status == StatusType.Active && p.Type == ContentType.News)
                    .Include(p => p.User)
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.Id)
                    .Select(p => new Content
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Image = p.Image,
                        CreatedAt = p.CreatedAt,
                        User = p.User,
                        Category = p.Category,
                        UserId = p.UserId,
                        CategoryId = p.CategoryId
                    }).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Category> GetSpecialCategories()
        {
            try
            {
                var data = _context.Categories
                    .Where(p => p.Status == StatusType.Active && p.IsSpecial)
                    .OrderByDescending(p => p.Id)
                    .Take(10)
                    .Select(p => new Category
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Image = p.Image

                    }).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Content> GetPopularNews()
        {
            try
            {
                List<Content>? data = _context.Contents
                    .Where(p => p.Status == StatusType.Active && p.Type == ContentType.News)
                    .Include(p => p.User)
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.Like)
                    .Select(p => new Content
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Image = p.Image,
                        CreatedAt = p.CreatedAt,
                        User = p.User,
                        Category = p.Category,
                        UserId = p.UserId,
                        CategoryId = p.CategoryId
                    }).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Content> GetLastBlogs()
        {
            try
            {
                List<Content>? data = _context.Contents
                    .Where(p => p.Status == StatusType.Active && p.Type == ContentType.Blog)
                    .Include(p => p.User)
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.Id)
                    .Select(p => new Content
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Image = p.Image,
                        CreatedAt = p.CreatedAt,
                        User = p.User,
                        Category = p.Category,
                        UserId = p.UserId,
                        CategoryId = p.CategoryId
                    }).ToList();

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Setting GetSetting(string key)
        {
            try
            {
                Setting item = _context.Setting.FirstOrDefault(p => p.Key.Equals(key));

                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Category GetCategory(int id)
        {
            try
            {
                var result = _context.Categories
                    .Where(p => p.Id == id && p.Status == StatusType.Active)
                    .Include(p => p.Contents)
                    .ThenInclude(x => x.User)
                    .Include(c => c.Contents)
                    .ThenInclude(t => t.Comments).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Content GetContent(int id)
        {
            try
            {
                var result = _context.Contents
                    .Where(p => p.Id == id && p.Status == StatusType.Active)
                    .Include(p => p.User)
                    .Include(p => p.Category)
                    .Include(p => p.Comments.Where(p => p.Status == StatusType.Active).OrderByDescending(p => p.Id))
                    .ThenInclude(p => p.User)
                    .FirstOrDefault();

                if (result != null)
                {
                    result.Visit += 1;
                    _context.SaveChanges();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public bool SaveComment(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
