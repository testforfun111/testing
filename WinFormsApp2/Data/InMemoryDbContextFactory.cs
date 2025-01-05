using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
namespace UnitTests.Data
{
    public class InMemoryDbContextFactory : dbContextFactory
    {
        public string name { get; set; }
        public InMemoryDbContextFactory() 
        {
            name = Guid.NewGuid().ToString();
        }
        public DataContext get_db_context()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(name);
            return new DataContext(builder.Options);
        }
    }
}
