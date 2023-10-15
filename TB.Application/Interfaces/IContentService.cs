using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.Models;

namespace TB.Application.Interfaces
{
    public interface IContentService
    {
        Task<bool> Create(Content model);
        Task<bool> Delete(int id);
        Task<Content> FindById(int id);
        Task<List<Content>> GetAll();
        Task<bool> Update(Content model);
    }
}
