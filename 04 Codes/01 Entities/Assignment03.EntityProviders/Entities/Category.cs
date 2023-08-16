using SharedLibrary.EntityProviders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Assignment03.EntityProviders;

[Table(nameof(Category))]
public class Category : BaseEntity
{
    #region [ Properties ]
    [Required]
    [DataType(DataType.Text)]
    public string Name { get; set; }
    #endregion

    #region [ Properties ]
    [NotMapped]
    [JsonIgnore]
    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; }
    #endregion
}
