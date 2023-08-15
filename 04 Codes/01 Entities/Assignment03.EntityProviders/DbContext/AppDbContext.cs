using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment03.EntityProviders;

public class AppDbContext : DbContext
{
    #region [ CTor ]
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

    }
    #endregion

    #region [ Properties - DbSet ]
    public DbSet<User> Users { get; set; }

    public DbSet<UserPhone> UsersPhones { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }  
    #endregion
}
