namespace Assignment03.EntityProviders;

public class ApiResponseModel
{
    #region [ Properties ]
    public bool Success { get; set; }

    public string Message { get; set; }

    public object Data { get; set; }
    #endregion
}