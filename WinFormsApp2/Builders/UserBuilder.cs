using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class UserBuilder
    {
        private int id;
        private string name;
        private string phone;
        private string address;
        private string email;
        private string login;
        private string password;
        private string role;
        public UserBuilder()
        {
            id = 1;
            name = "test";
            phone = "test";
            address = "test";
            email = "test";
            login = "test";
            password = "test";
            role = "test";
        }
        public UserBuilder WithId(int Id)
        {
            id = Id;
            return this;
        }
        public UserBuilder WithName(string Name)
        {
            name = Name;
            return this;
        }
        public UserBuilder WithPhone(string Phone)
        {
            phone = Phone;
            return this;
        }
        public UserBuilder WithAddress(string Address)
        {
            address = Address;
            return this;
        }
        public UserBuilder WithEmail(string Email)
        {
            email = Email;
            return this;
        }
        public UserBuilder WithLogin(string Login)
        {
            login = Login;
            return this;
        }
        public UserBuilder WithPassword(string Password)
        {
            password = Password;
            return this;
        }
        public UserBuilder WithRole(string Role)
        {
            role = Role;
            return this;
        }
        public User BuildCoreModel()
        {
            return new User(id, name, phone, address, email, login, password, role);
        }
    }
}
