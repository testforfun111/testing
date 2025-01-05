using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class pgSqlDbContextFactory : dbContextFactory
    {
        public IConfiguration config  { get; set; }
        public string connectionStr;
        public pgSqlDbContextFactory(IConfiguration config)
        {
            this.config = config;
        }
        public pgSqlDbContextFactory(string connectionStr)
        {
            this.connectionStr = connectionStr;
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseNpgsql(connectionStr);
            var context = new DataContext(builder.Options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Database.Migrate();

            //using (var context = new DataContext(builder.Options))
            //{
            //    try
            //    {
            //        context.Database.Migrate();
            //        Console.WriteLine("Database migration completed successfully.");
            //    }
            //    catch (System.Net.Sockets.SocketException ex)
            //    {
            //        Console.WriteLine($"SocketException: {ex.Message}");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"An error occurred during migration: {ex.Message}");
            //    }
            //}

        }


        public DataContext get_db_context()
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            //builder.UseNpgsql(config.GetSection("PostgreSQL").GetSection("ConnectionString").Value);
            if (config != null)
            {
                builder.UseNpgsql(config.GetSection("PostgreSQL").GetSection("ConnectionString").Value);
            }
            else
            {
                builder.UseNpgsql(connectionStr);
            }
            return new DataContext(builder.Options);
        }

    }
}
