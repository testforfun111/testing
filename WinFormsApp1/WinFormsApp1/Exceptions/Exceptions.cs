using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string inf = "Не существует такого пользователя") : base(inf) { }
        public UserNotFoundException(Exception inner, string inf = "Не существует такого пользователя") : base(inf, inner) { }
    }
    public class UserNotMatchPasswordException : Exception
    {
        public UserNotMatchPasswordException(string inf = "неверный пароль") : base (inf) { }
    }

    public class UserExistException : Exception
    {
        public UserExistException(string inf = "уже сущ") : base(inf) { }
    }
    public class ProductExistException : Exception
    {
        public ProductExistException(string inf = "уже сущ") : base(inf) { }
    }
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string inf = "Не существует такого") : base(inf) { }
    }
    public class OrderExistException : Exception
    {
        public OrderExistException(string inf = "Уже существует") : base(inf) { }
    }
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(string inf = "Не существует такой") : base(inf) { }
    }
    public class ItemOrderExistException : Exception
    {
        public ItemOrderExistException(string inf = "Уже существует") : base(inf) { }
    }
    public class ItemOrderNotFoundException : Exception
    {
        public ItemOrderNotFoundException(string inf = "Не существует такой") : base(inf) { }
    }
    public class CartExistException : Exception
    {
        public CartExistException(string inf = "Уже существует") : base(inf) { }
    }
    public class CartNotFoundException : Exception
    {
        public CartNotFoundException(string inf = "Не существует такого") : base(inf) { }
    }
    public class ItemCartExistException : Exception
    {
        public ItemCartExistException(string inf = "Уже существует") : base(inf) { }
    }
    public class ItemCartNotFoundException : Exception
    {
        public ItemCartNotFoundException(string inf = "Не существует такого") : base(inf) { }
    }
}
