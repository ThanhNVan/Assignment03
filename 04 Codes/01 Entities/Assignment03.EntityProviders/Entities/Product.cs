using SharedLibrary.EntityProviders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Assignment03.EntityProviders;

[Table(nameof(Product))]
public class Product : BaseEntity
{
    #region [ Properties ]
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public string Weight { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int InStock { get; set; }
    #endregion

    #region [ Properties - FK]
    public string CategoryId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(CategoryId))]
    [InverseProperty("Products")]
    public Category Category { get; set; }
    #endregion

    #region [ Properties - Virtual]
    [NotMapped]
    [JsonIgnore]
    [InverseProperty("Product")]
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    #endregion
}
