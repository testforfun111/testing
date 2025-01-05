using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;

namespace UnitTests.ObjectMothers
{
    public class UserObjectMother
    {
        public UserBuilder CreateGuest()
        {
            return new UserBuilder()
                .WithRole("Guest")
                .WithName("Guest");
        }
        public UserBuilder CreateClient ()
        {
            return new UserBuilder()
                .WithRole("Client")
                .WithName("Max")
                .WithLogin("client")
                .WithPassword("client");
        }
        public UserBuilder CreateAdmin()
        {
            return new UserBuilder()
                .WithRole("Admin")
                .WithName("admin")
                .WithLogin("admin")
                .WithPassword("admin");
        }
        public UserBuilder CreateUser(int id, string name)
        {
            return new UserBuilder()
                .WithName(name)
                .WithId(id)
                .WithRole("Client");
        }
    }
}
