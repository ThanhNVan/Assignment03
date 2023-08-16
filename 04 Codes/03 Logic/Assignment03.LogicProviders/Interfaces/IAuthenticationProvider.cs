using Assignment03.EntityProviders;

namespace Assignment03.LogicProviders;

public interface IAuthenticationProvider
{
    #region [ Methods - Generate Token ]
    //string GenerateRefreshToken();

    TokenModel GenerateToken(User user);
    #endregion

    #region [ Methods - Hash ]
    string HashPassword(string password);
    #endregion
}
