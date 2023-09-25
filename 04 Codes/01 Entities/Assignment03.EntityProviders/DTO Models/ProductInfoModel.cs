namespace Assignment03.EntityProviders;

public class ProductInfoModel
{
    #region [ Properties ]
    public string Id { get; set; }

    public string Name { get; set; }

    public string Weight { get; set; }

    public decimal Price { get; set; }

    public int InStock { get; set; }

    public string Category { get; set; }
    #endregion
}
