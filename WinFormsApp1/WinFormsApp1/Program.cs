using Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Interfaces;
using UI;
using Microsoft.VisualBasic.ApplicationServices;
using Data;
namespace WinFormsApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                Console.WriteLine("abc");
                var services = scope.ServiceProvider;
                try
                {
                    // Resolve TechUI or any other services to start your application
                    var techUI = services.GetRequiredService<TechUI>();
                    techUI.openMainView();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Retrieve configuration
                    var configuration = context.Configuration;

                    // Register the DbContext with a connection string from appsettings.json
                    services.AddDbContext<DataContext>(options =>
                        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

                    // Register your repositories
                    services.AddScoped<IUserRepository, UserRepository>();
                    services.AddScoped<IProductRepository, ProductRepository>();
                    services.AddScoped<IOrderRepository, OrderRepository>();
                    services.AddScoped<ICartRepository, CartRepository>();
                    services.AddScoped<IItemOrderRepository, ItemOrderRepository>();
                    services.AddScoped<IItemCartRepository, ItemCartRepository>();

                    services.AddScoped<UserService>();
                    services.AddScoped<ProductService>();
                    services.AddScoped<OrderService>();
                    services.AddScoped<CartService>();
                    services.AddScoped<ItemCartService>();
                    services.AddScoped<ItemOrderService>();


                    // Register other services and the UI component
                    services.AddScoped<TechUI>();
                });
    }
}