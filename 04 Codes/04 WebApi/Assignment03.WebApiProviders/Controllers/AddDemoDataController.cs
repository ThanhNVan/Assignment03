using Assignment03.EntityProviders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment03.WebApiProviders;

//[ApiController]
//[Route("Api/V1/[controller]")]
public class AddDemoDataController : ControllerBase
{
    #region [ Fields ]
    private readonly IDbContextFactory<AppDbContext> _dbContext;
    #endregion

    #region [ CTor ]
    public AddDemoDataController(IDbContextFactory<AppDbContext> dbContext) {
        _dbContext = dbContext;
        this.LoadAddData();
    }
    #endregion

    #region [ Properties ]
    public IList<Category> Categories { get; set; } = new List<Category>();
    public IList<Order> Orders { get; set; } = new List<Order>();
    public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public IList<Product> Products { get; set; } = new List<Product>();
    public IList<User> Users { get; set; } = new List<User>();
    public IList<UserPhone> UserPhones { get; set; } = new List<UserPhone>();
    #endregion

    [HttpGet]
    public async Task<IActionResult> AddDemoDataAsync() {
        try {

            var dbContext = await this._dbContext.CreateDbContextAsync();
            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () => {
                using var transaction = await dbContext.Database.BeginTransactionAsync();
                try {
                    await dbContext.AddRangeAsync(this.Users);
                    await dbContext.AddRangeAsync(this.UserPhones);
                    await dbContext.AddRangeAsync(this.Categories);
                    await dbContext.AddRangeAsync(this.Products);
                    await dbContext.AddRangeAsync(this.Orders);
                    await dbContext.AddRangeAsync(this.OrderItems);

                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                } catch (Exception ex) {
                    var aa = ex.Message;
                    await transaction.RollbackAsync();
                    throw;
                }
            });
            return Ok();

        } catch (Exception ex) {
            return BadRequest();
            throw;
        }
    }
    private void LoadAddData() {
        LoadUser();
        LoadUserPhone();
        LoadCategory();
        LoadProduct();
        LoadOrder();
        LoadOrderItem();
    }

    private void LoadUser() {
        var user_0 = Factory.CreateUser(firstname: "Admin",
                                        lastname: "Van",
                                        email: "Admin.van@gmail.com",
                                        password: "123456@@",
                                        role: (int)RoleEnums.Admin);
        
        var user_1 = Factory.CreateUser(firstname: "Manager",
                                        lastname: "Van",
                                        email: "Manager.van@gmail.com",
                                        password: "123456@@",
                                        role: (int)RoleEnums.Manager);
        
        var user_2 = Factory.CreateUser(firstname: "Employee",
                                        lastname: "Van",
                                        email: "Employee.van@gmail.com",
                                        password: "123456@@",
                                        role: (int)RoleEnums.Employee);
        
        var user_3 = Factory.CreateUser(firstname: "Employee1",
                                        lastname: "Van",
                                        email: "Employee1.van@gmail.com",
                                        password: "123456@@",
                                        role: (int)RoleEnums.Employee);
        
        var user_4 = Factory.CreateUser(firstname: "An",
                                        lastname: "Tran",
                                        email: "an.tran@gmail.com",
                                        password: "123456@@",
                                        role: (int)RoleEnums.Employee);
        this.Users.Add(user_0);
        this.Users.Add(user_1);
        this.Users.Add(user_2);
        this.Users.Add(user_3);
        this.Users.Add(user_4);
    }

    private void LoadUserPhone() {
        var userPhone_0 = Factory.CreateUserPhone(phone: "0123450000", userId: Users[0].Id);
        var userPhone_1 = Factory.CreateUserPhone(phone: "0123450001", userId: Users[0].Id);
        var userPhone_2 = Factory.CreateUserPhone(phone: "0123450002", userId: Users[1].Id);
        var userPhone_3 = Factory.CreateUserPhone(phone: "0123450003", userId: Users[1].Id);
        var userPhone_4 = Factory.CreateUserPhone(phone: "0123450004", userId: Users[2].Id);
        var userPhone_5 = Factory.CreateUserPhone(phone: "0123450005", userId: Users[2].Id);
        var userPhone_6 = Factory.CreateUserPhone(phone: "0123450006", userId: Users[3].Id);
        var userPhone_7 = Factory.CreateUserPhone(phone: "0123450007", userId: Users[3].Id);
        var userPhone_8 = Factory.CreateUserPhone(phone: "0123450008", userId: Users[4].Id);
        var userPhone_9 = Factory.CreateUserPhone(phone: "0123450009", userId: Users[4].Id);
        this.UserPhones.Add(userPhone_0);   
        this.UserPhones.Add(userPhone_1);   
        this.UserPhones.Add(userPhone_2);   
        this.UserPhones.Add(userPhone_3);   
        this.UserPhones.Add(userPhone_4);   
        this.UserPhones.Add(userPhone_5);   
        this.UserPhones.Add(userPhone_6);   
        this.UserPhones.Add(userPhone_7);   
        this.UserPhones.Add(userPhone_8);   
        this.UserPhones.Add(userPhone_9);   
    }

    private void LoadCategory() {
        var category_0 = Factory.CreateCategory("Food");
        var category_1 = Factory.CreateCategory("Drinks");
        var category_2 = Factory.CreateCategory("Furnitures");
        var category_3 = Factory.CreateCategory("Electronics");

        this.Categories.Add(category_0);
        this.Categories.Add(category_1);
        this.Categories.Add(category_2);
        this.Categories.Add(category_3);
    }

    private void LoadProduct() {
        var product_0 = Factory.CreateProduct(name: "Hamburger",
                                                weight: "300g",
                                                price: 5000,
                                                instock: 40,
                                                categoryId: Categories[0].Id);
        
        var product_1 = Factory.CreateProduct(name: "Rice",
                                                weight: "25kg",
                                                price: 500000,
                                                instock: 14,
                                                categoryId: Categories[0].Id);
        
        var product_2 = Factory.CreateProduct(name: "Bun Bo",
                                                weight: "500g",
                                                price: 50000,
                                                instock: 20,
                                                categoryId: Categories[0].Id);
        
        var product_3 = Factory.CreateProduct(name: "Pepsi",
                                                weight: "330ml",
                                                price: 10000,
                                                instock: 20,
                                                categoryId: Categories[1].Id);
        
        var product_4 = Factory.CreateProduct(name: "Coke",
                                                weight: "330ml",
                                                price: 10000,
                                                instock: 11,
                                                categoryId: Categories[1].Id);
        
        var product_5 = Factory.CreateProduct(name: "Huda Hue",
                                                weight: "330ml",
                                                price: 15000,
                                                instock: 21,
                                                categoryId: Categories[1].Id);
        
        var product_6 = Factory.CreateProduct(name: "Chair",
                                                weight: "7kg",
                                                price: 700000,
                                                instock: 20,
                                                categoryId: Categories[2].Id);
        
        var product_7 = Factory.CreateProduct(name: "Table",
                                                weight: "10kg",
                                                price: 500000,
                                                instock: 21,
                                                categoryId: Categories[2].Id);
        
        var product_8 = Factory.CreateProduct(name: "Monitor Stand",
                                                weight: "1.2kg",
                                                price: 150000,
                                                instock: 19,
                                                categoryId: Categories[2].Id);
        
        var product_9 = Factory.CreateProduct(name: "Refrigerator",
                                                weight: "30kg",
                                                price: 7000000,
                                                instock: 20,
                                                categoryId: Categories[3].Id);
        
        var product_10 = Factory.CreateProduct(name: "Television",
                                                weight: "10kg",
                                                price: 5000000,
                                                instock: 21,
                                                categoryId: Categories[3].Id);
        
        var product_11 = Factory.CreateProduct(name: "Washing Machine",
                                                weight: "12kg",
                                                price: 8000000,
                                                instock: 19,
                                                categoryId: Categories[3].Id);

        this.Products.Add(product_0);
        this.Products.Add(product_1);
        this.Products.Add(product_2);
        this.Products.Add(product_3);
        this.Products.Add(product_4);
        this.Products.Add(product_5);
        this.Products.Add(product_6);
        this.Products.Add(product_7);
        this.Products.Add(product_8);
        this.Products.Add(product_9);
        this.Products.Add(product_10);
        this.Products.Add(product_11);
    }

    private void LoadOrder() {
        var order_0 = Factory.CreateOrder(userId: Users[0].Id, 123);
        var order_1 = Factory.CreateOrder(userId: Users[0].Id, 122);

        var order_2 = Factory.CreateOrder(userId: Users[1].Id, 124);
        var order_3 = Factory.CreateOrder(userId: Users[1].Id, 125);
        
        var order_4 = Factory.CreateOrder(userId: Users[2].Id, 115);
        var order_5 = Factory.CreateOrder(userId: Users[2].Id, 114);
        
        var order_6 = Factory.CreateOrder(userId: Users[3].Id, 118);
        var order_7 = Factory.CreateOrder(userId: Users[3].Id, 108);
        
        var order_8 = Factory.CreateOrder(userId: Users[4].Id, 198);
        var order_9 = Factory.CreateOrder(userId: Users[4].Id, 148);

        Orders.Add(order_0);
        Orders.Add(order_1);
        Orders.Add(order_2);
        Orders.Add(order_3);
        Orders.Add(order_4);
        Orders.Add(order_5);
        Orders.Add(order_6);
        Orders.Add(order_7);
        Orders.Add(order_8);
        Orders.Add(order_9);
    } 

    private void LoadOrderItem() {
        var orderItem_0_1 = Factory.CreateOrderItem(orderId: Orders[0].Id, productId: Products[0].Id, quantity: 2, discount: 2.1m);
        var orderItem_0_2 = Factory.CreateOrderItem(orderId: Orders[0].Id, productId: Products[1].Id, quantity: 1, discount: 2.1m);
        var orderItem_0_3 = Factory.CreateOrderItem(orderId: Orders[0].Id, productId: Products[3].Id, quantity: 1, discount: 2.1m);
        OrderItems.Add(orderItem_0_1);
        OrderItems.Add(orderItem_0_2);
        OrderItems.Add(orderItem_0_3);

        var orderItem_1_1 = Factory.CreateOrderItem(orderId: Orders[1].Id, productId: Products[1].Id, quantity: 3, discount: 2.1m);
        var orderItem_1_2 = Factory.CreateOrderItem(orderId: Orders[1].Id, productId: Products[2].Id, quantity: 3, discount: 5.0m);
        var orderItem_1_3 = Factory.CreateOrderItem(orderId: Orders[1].Id, productId: Products[4].Id, quantity: 1, discount: 5.0m);
        OrderItems.Add(orderItem_1_1);
        OrderItems.Add(orderItem_1_2);
        OrderItems.Add(orderItem_1_3);

        var orderItem_2_1 = Factory.CreateOrderItem(orderId: Orders[2].Id, productId: Products[2].Id, quantity: 3, discount: 3.1m);
        var orderItem_2_2 = Factory.CreateOrderItem(orderId: Orders[2].Id, productId: Products[3].Id, quantity: 2, discount: 3.1m);
        var orderItem_2_3 = Factory.CreateOrderItem(orderId: Orders[2].Id, productId: Products[5].Id, quantity: 1, discount: 3.1m);
        OrderItems.Add(orderItem_2_1);
        OrderItems.Add(orderItem_2_2);
        OrderItems.Add(orderItem_2_3);
        
        var orderItem_3_1 = Factory.CreateOrderItem(orderId: Orders[3].Id, productId: Products[3].Id, quantity: 3, discount: 3.3m);
        var orderItem_3_2 = Factory.CreateOrderItem(orderId: Orders[3].Id, productId: Products[4].Id, quantity: 2, discount: 3.4m);
        var orderItem_3_3 = Factory.CreateOrderItem(orderId: Orders[3].Id, productId: Products[6].Id, quantity: 1, discount: 3.5m);
        OrderItems.Add(orderItem_3_1);
        OrderItems.Add(orderItem_3_2);
        OrderItems.Add(orderItem_3_3);
        
        var orderItem_4_1 = Factory.CreateOrderItem(orderId: Orders[4].Id, productId: Products[4].Id, quantity: 3, discount: 3.3m);
        var orderItem_4_2 = Factory.CreateOrderItem(orderId: Orders[4].Id, productId: Products[5].Id, quantity: 2, discount: 3.4m);
        var orderItem_4_3 = Factory.CreateOrderItem(orderId: Orders[4].Id, productId: Products[7].Id, quantity: 1, discount: 3.5m);
        OrderItems.Add(orderItem_4_1);
        OrderItems.Add(orderItem_4_2);
        OrderItems.Add(orderItem_4_3);

        var orderItem_5_1 = Factory.CreateOrderItem(orderId: Orders[5].Id, productId: Products[4].Id, quantity: 3, discount: 3.3m);
        var orderItem_5_2 = Factory.CreateOrderItem(orderId: Orders[5].Id, productId: Products[5].Id, quantity: 2, discount: 3.4m);
        var orderItem_5_3 = Factory.CreateOrderItem(orderId: Orders[5].Id, productId: Products[7].Id, quantity: 1, discount: 3.5m);
        OrderItems.Add(orderItem_5_1);
        OrderItems.Add(orderItem_5_2);
        OrderItems.Add(orderItem_5_3);

        var orderItem_6_1 = Factory.CreateOrderItem(orderId: Orders[6].Id, productId: Products[5].Id, quantity: 3, discount: 3.3m);
        var orderItem_6_2 = Factory.CreateOrderItem(orderId: Orders[6].Id, productId: Products[6].Id, quantity: 2, discount: 3.4m);
        var orderItem_6_3 = Factory.CreateOrderItem(orderId: Orders[6].Id, productId: Products[8].Id, quantity: 1, discount: 3.5m);
        OrderItems.Add(orderItem_6_1);
        OrderItems.Add(orderItem_6_2);
        OrderItems.Add(orderItem_6_3);
        
        var orderItem_7_1 = Factory.CreateOrderItem(orderId: Orders[7].Id, productId: Products[6].Id, quantity: 3, discount: 3.3m);
        var orderItem_7_2 = Factory.CreateOrderItem(orderId: Orders[7].Id, productId: Products[7].Id, quantity: 2, discount: 3.4m);
        var orderItem_7_3 = Factory.CreateOrderItem(orderId: Orders[7].Id, productId: Products[9].Id, quantity: 1, discount: 3.5m);
        OrderItems.Add(orderItem_7_1);
        OrderItems.Add(orderItem_7_2);
        OrderItems.Add(orderItem_7_3);
        
        var orderItem_8_1 = Factory.CreateOrderItem(orderId: Orders[8].Id, productId: Products[7].Id, quantity: 3, discount: 3.3m);
        var orderItem_8_2 = Factory.CreateOrderItem(orderId: Orders[8].Id, productId: Products[8].Id, quantity: 2, discount: 3.4m);
        var orderItem_8_3 = Factory.CreateOrderItem(orderId: Orders[8].Id, productId: Products[10].Id, quantity: 1, discount: 3.5m);
        OrderItems.Add(orderItem_8_1);
        OrderItems.Add(orderItem_8_2);
        OrderItems.Add(orderItem_8_3);
        
        var orderItem_9_1 = Factory.CreateOrderItem(orderId: Orders[9].Id, productId: Products[8].Id, quantity: 3, discount: 3.3m);
        var orderItem_9_2 = Factory.CreateOrderItem(orderId: Orders[9].Id, productId: Products[9].Id, quantity: 2, discount: 3.4m);
        var orderItem_9_3 = Factory.CreateOrderItem(orderId: Orders[9].Id, productId: Products[11].Id, quantity: 1, discount: 3.5m);
        OrderItems.Add(orderItem_9_1);
        OrderItems.Add(orderItem_9_2);
        OrderItems.Add(orderItem_9_3);
       
    }
}
