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
    public class SiteService:ISiteService
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
                        Id = p.Id ,
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
                        Id = p.Id , 
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
    }
}
