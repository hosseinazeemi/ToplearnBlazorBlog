using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.Models;

namespace TB.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<bool> Create(Category category);
        Task<bool> Delete(int id);
        Task<Category> FindById(int id);
        Task<List<Category>> GetAll();
        Task<bool> Update(Category category);
    }
}
