using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Data
{
    public class pgSqlDbContextFactory : dbContextFactory
    {
        public IConfiguration config  { get; set; }
        public pgSqlDbContextFactory(IConfiguration config)
        {
            this.config = config;
        }
        public DataContext get_db_context()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseNpgsql(config.GetSection("PostgreSQL").GetSection("ConnectionString").Value);
            return new DataContext(builder.Options);
        }

    }
}
