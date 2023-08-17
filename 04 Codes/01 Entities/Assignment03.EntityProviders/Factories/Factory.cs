using System.Security.Cryptography;
using System.Text;

namespace Assignment03.EntityProviders;

public static class Factory
{
    public static Category CreateCategory(string name) { 
        var category = new Category();
        category.Name = name; 
        
        return category;
    }

    public static Order CreateOrder(string userId, decimal freight) {
        var order = new Order();
        order.OrderDate = DateTime.UtcNow;
        order.RequiredDate = DateTime.UtcNow.AddDays(10);
        order.ShippedDate = DateTime.UtcNow.AddDays(7);
        order.Freight = freight;
        order.UserId = userId;

        return order;
    }

    public static OrderItem CreateOrderItem(string orderId, string productId, int quantity, decimal discount) {
        var orderItem = new OrderItem();
        orderItem.OrderId = orderId;
        orderItem.ProductId = productId;
        orderItem.Quantity = quantity;
        orderItem.Discount = discount;

        return orderItem;
    }

    public static Product CreateProduct(string name, string weight, decimal price, int instock, string categoryId) {
        var product = new Product();
        product.Name = name;
        product.Weight = weight;
        product.Price = price;
        product.InStock = instock;
        product.CategoryId = categoryId;

        return product;
    }

    public static User CreateUser(string firstname, string lastname, string email, string password, int role) {
        var user = new User();
        user.Firstname = firstname;
        user.Lastname = lastname;
        user.Email = email;
        user.PasswordHash = HashPassword(password);
        user.Role = role;

        return user;
    }

    public static UserPhone CreateUserPhone(string phone, string userId) {
        var userPhone = new UserPhone();
        userPhone.Phone = phone;
        userPhone.UserId = userId;

        return userPhone;
    }

    public static string HashPassword(string password) {
        var result = string.Empty;
        using (var sha512 = SHA512.Create()) {
            var passwordData = Encoding.UTF8.GetBytes(password);
            var hashValue = sha512.ComputeHash(passwordData);
            foreach (byte x in hashValue) {
                result += string.Format("{0:x2}", x);
            }
        }
        return result;
    }
}
