using TB.Domain.Models;

namespace TB.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> Create(User model);
        Task<bool> Delete(int id);
        Task<User> FindById(int id);
        Task<List<User>> GetAll();
        Task<bool> Update(User model);
    }
}
