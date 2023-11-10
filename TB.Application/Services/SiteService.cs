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
    }
}
