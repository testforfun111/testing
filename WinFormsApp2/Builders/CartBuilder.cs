using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class CartBuilder
    {
        private int id;
        private DateTime data_created;
        private int id_user;
        public CartBuilder()
        {
            id = 1;
            //data_created = DateTime.Parse("10-10-24");
            data_created = DateTime.Now.ToUniversalTime();
            id_user = 1;
        }
        public CartBuilder WithId(int Id)
        {
            id = Id;
            return this;
        }
        public CartBuilder WithDatacreated(DateTime Data_created)
        {
            data_created = Data_created;
            return this;
        }
        public CartBuilder WithIdUser(int Id_user)
        {
            id_user = Id_user;
            return this;
        }
        public Cart BuildCoreModel()
        {
            return new Cart(id, data_created, id_user);
        }
    }
}
