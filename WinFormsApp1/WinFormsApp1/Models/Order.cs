using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum Status
    {
        None,
        Init,
        Delivering,
        Delivered,
    }
    public class Order
    {

        private int id;
        private Status status;
        private DateTime data_created;
        private int id_user;

        public int Id { get => id; set => id = value; }
        public Status Status { get => status; set => status = value; }

        public DateTime Data_created { get => data_created; set => data_created = value; }
        public int Id_user { get => id_user; set => id_user = value; }

        public Order(int id, Status status, DateTime data_created, int id_user)
        {
            this.id = id;
            this.status = status;
            this.data_created = data_created;
            this.id_user = id_user;
        }
    }
}
