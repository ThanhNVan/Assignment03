using SharedLibrary.EntityProviders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Assignment03.EntityProviders;

[Table(nameof(Order))]
public class Order : BaseEntity
{
	#region [ Properties ]
	[Required]
    [DataType(DataType.DateTime)]
    public DateTime OrderDate { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime? RequiredDate { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime? ShippedDate { get; set; }

    public decimal Freight { get; set; }
    #endregion

    #region [ Properties - FK ]
    public string UserId { get; set; }

    [Required]
    [JsonIgnore]
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    #endregion

    #region [ Properties - Virtual]
    [NotMapped]
    [JsonIgnore]
    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set;}
    #endregion
}
