using Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;
using System.Xml.Linq;

namespace UI
{
    public class TechUI
    {
        private UserService _userService;
        private ProductService _productService;
        private CartService _cartService;
        private ItemCartService _itemCartService;
        private OrderService _orderService;
        private ItemOrderService _itemOrderService;

        public TechUI(UserService userService, ProductService productService, OrderService orderService, CartService cartService, ItemOrderService itemOrderSerivce, ItemCartService itemCartService)
        {
            _userService = userService;
            _productService = productService;
            _orderService = orderService;
            _cartService = cartService;
            _itemCartService = itemCartService;
            _itemOrderService = itemOrderSerivce;
        }
        public void openMainView()
        {
            int choice;
            do
            {
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. Guest");
                Console.WriteLine("2. Sign in");
                Console.WriteLine("3. Sign up");
                Console.WriteLine("Command: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        //forGuest();
                        break;
                    case 2:
                        signIn();
                        break;
                    case 3:
                        signUp();
                        break;
                    case 0:
                        //exit();
                        break;
                    default:
                        Console.WriteLine("don't exists");
                        break;
                }
            } while (choice != 0);
        }
        public void signIn()
        {
            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
            User user = _userService.LogIn(username, password);
            if (user != null)
            {
                Console.WriteLine(user.Name);
            }
            else
            {
                Console.WriteLine("username or password invalid");
            }
        }
        public void signUp()
        {
            Console.WriteLine("Enter username: ");
            string login, password, name, address, email, phone;
            Console.Write("Input login: ");
            login = Console.ReadLine();

            Console.Write("Input password: ");
            password = Console.ReadLine();

            Console.Write("Input name: ");
            name = Console.ReadLine();

            Console.Write("Input address: ");
            address = Console.ReadLine();

            Console.Write("Input email: ");
            email = Console.ReadLine();
            Console.Write("Input phone: ");
            phone = Console.ReadLine();

            if (login == "" || password == "" || name == "" || address == "" || email == "" || phone == "")
                throw new Exception("Info user input wrong!!!");
            User user = _userService.Register(name, phone, address, email, login, password, "Client");
            Console.WriteLine("Success");
        }
    }

}
