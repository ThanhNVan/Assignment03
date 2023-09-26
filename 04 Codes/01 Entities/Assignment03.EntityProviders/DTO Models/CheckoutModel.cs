namespace Assignment03.EntityProviders;

public class CheckoutModel
{
    #region [ Properties ]
    public string Email { get; set; }

    public List<Cart> Carts { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }
    #endregion
}
