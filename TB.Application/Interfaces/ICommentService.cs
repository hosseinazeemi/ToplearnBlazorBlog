using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.Models;

namespace TB.Application.Interfaces
{
    public interface ICommentService
    {
        Task<bool> Create(Comment model);
        Task<bool> Delete(int id);
        Task<Comment> FindById(int id);
        Task<List<Comment>> GetAllComments(int contentId = 0);
        Task<bool> Update(Comment model);
    }
}
