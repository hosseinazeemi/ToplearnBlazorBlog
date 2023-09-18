using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.Models;

namespace TB.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Content> Contents { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Setting> Setting { get; set; }

        int SaveChanges(bool acceptAll);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAll , CancellationToken cancel = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancel = new CancellationToken());
    }
}
