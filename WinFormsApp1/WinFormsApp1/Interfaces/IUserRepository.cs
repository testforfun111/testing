using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);

        User GetUser(string login);

        void AddUser(User user);

        void DelUser(User user);

        void UpdateUser(User user);

        ICollection<User> GetAll();

        int CountAllUsers();
    }
}
