using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using System.Data;
using Interfaces;
using Data;
using Models;

namespace Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;
        
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public User GetUser(int id)
        {
            return _context.users.Where(p => p.Id == id).FirstOrDefault();
        }

        public User GetUser(string login)
        {
            return _context.users.Where(p => p.Login == login).FirstOrDefault();
        }

        public void AddUser(User user)
        {
            List<User>? lst = _context.users.Count() > 0 ? _context.users.ToList() : null;
            int maxid = 0;
            if (lst != null)
            {
                foreach (User temp in lst)
                    if (temp.Id > maxid)
                        maxid = temp.Id;
            }
            user.Id = maxid + 1;
            _context.users.Add(user);
            _context.SaveChanges();
        }
        public void DelUser(User user)
        {
            User userToRemove = _context.users.FirstOrDefault(u => u.Id == user.Id);
            if (userToRemove != null)
            {
                _context.Remove(userToRemove);
            }
            _context.SaveChanges();
        }
        public void UpdateUser(User user)
        {
            var userToUpdate = _context.users.FirstOrDefault(u => u.Id == user.Id);
            if (userToUpdate != null)
            {
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                userToUpdate.Login = user.Login;
                userToUpdate.Address = user.Address;
                userToUpdate.Phone = user.Phone;
                _context.SaveChanges();
            }
        }

        public ICollection<User> GetAll()
        {
            return _context.users.ToList();
        }

        public int CountAllUsers()
        {
            return _context.users.Count();
        }
    }
}
