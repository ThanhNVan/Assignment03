using Microsoft.EntityFrameworkCore;
using SharedLibrary.EntityProviders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Assignment03.EntityProviders;

[Table(nameof(User))]
[Index(nameof(Email), IsUnique = true)]
public class User : BaseEntity
{
    #region [ Properties ]
    [Required]
    [DataType(DataType.Text)]
    public string Firstname { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string Lastname { get; set; }

    [NotMapped]
    [JsonIgnore]
    public string Fullname => Firstname + " " + Lastname;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string PasswordHash { get; set; }
    
    [Required]
    public int Role { get; set; }
    #endregion

    #region [ Properties - Virtual]
    [JsonIgnore]
    [NotMapped]
    [InverseProperty("User")]
    public virtual ICollection<UserPhone>? UserPhones { get; set; }

    [JsonIgnore]
    [NotMapped]
    [InverseProperty("User")]
    public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
    #endregion
}
