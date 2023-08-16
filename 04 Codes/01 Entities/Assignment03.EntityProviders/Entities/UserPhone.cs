using Microsoft.EntityFrameworkCore;
using SharedLibrary.EntityProviders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Assignment03.EntityProviders;

[Table(nameof(UserPhone))]
[Index(nameof(Phone), nameof(UserId), IsUnique = true)]
public class UserPhone : BaseEntity
{
    #region [ Properties ]
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }
    #endregion

    #region [ Properties - FK ]
    public string UserId { get; set; }

    [Required]
    [JsonIgnore]
    [ForeignKey(nameof(UserId))]
    [InverseProperty("UserPhones")]
    public User User { get; set; }
    #endregion
}
