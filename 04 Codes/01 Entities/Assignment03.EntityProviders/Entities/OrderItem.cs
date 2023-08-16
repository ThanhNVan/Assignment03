using Microsoft.EntityFrameworkCore;
using SharedLibrary.EntityProviders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Assignment03.EntityProviders;

[Table(nameof(OrderItem))]
[Index(nameof(OrderId), nameof(ProductId), IsUnique = true)]
public class OrderItem : BaseEntity
{
	#region [ Properties ]

	[Required]
	public int Quantity { get; set; }

	public decimal Discount { get; set; }
	#endregion

	#region [ Properties - FK ]
	public string OrderId { get; set; }

	[Required]
	[JsonIgnore]
	[ForeignKey(nameof(OrderId))]
    [InverseProperty("OrderItems")]
    public Order Order { get; set; }
	

	public string ProductId { get; set; }

	[Required]
	[JsonIgnore]
	[ForeignKey(nameof(ProductId))]
    [InverseProperty("OrderItems")]
    public Product Product { get; set; }
	#endregion
}
