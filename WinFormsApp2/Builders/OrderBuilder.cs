using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class OrderBuilder
    {
        private int id;
        private Status status;
        private DateTime data_created;
        private int id_user;
        public OrderBuilder()
        {
            id = 1;
            status = Status.Init;
            //data_created = DateTime.Parse("10-10-24");
            data_created = DateTime.Now.ToUniversalTime();
            id_user = 1;
        }
        public OrderBuilder WithId(int Id)
        {
            id = Id;
            return this;
        }
        public OrderBuilder WithStatus(Status sTatus)
        {
            status = sTatus;
            return this;
        }
        public OrderBuilder WithDatacreated(DateTime Data_created)
        {
            data_created = Data_created;
            return this;
        }
        public OrderBuilder WithIdUser(int Id_user)
        {
            id_user = Id_user;
            return this;
        }
        public Order BuildCoreModel()
        {
            return new Order(id, status, data_created, id_user);
        }
    }
}
