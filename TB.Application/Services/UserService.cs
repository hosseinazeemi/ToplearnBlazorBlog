using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB.Application.Interfaces;
using TB.Domain.Models;

namespace TB.Application.Services
{
    public class UserService : IUserService
    {
        private IAppDbContext _context;
        public UserService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(User user)
        {
            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
                if (user.Id > 0)
                    return true;

                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<bool> Update(User user)
        {
            _context.Users.Update(user);

            try
            {
                await _context.SaveChangesAsync();
                if (user.Id > 0)
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
            //User model = new User
            //{
            //    Id = id
            //};
            User model = _context.Users.FirstOrDefault(p => p.Id == id);
            if (model != null)
            {
                _context.Users.Remove(model);

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
            throw new Exception("Not Found User");
        }
        public async Task<List<User>> GetAll()
        {
            try
            {
                List<User> result = _context.Users.ToList();

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }

        }
        public async Task<User> FindById(int id)
        {
            try
            {
                User result = _context.Users.FirstOrDefault(p => p.Id == id);

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }

        }

        public async Task<User> FindByEmail(string email)
        {

            try
            {
                User result = _context.Users.FirstOrDefault(p => p.Email.Equals(email));

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }
    }
}
