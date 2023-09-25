using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public class UserLogicProviders : BaseLogicProvider<User, IUserDataProviders>, IUserLogicProviders
{
    #region [ Fields ]
    private readonly IAuthenticationProvider _authentication;
    private readonly IRefreshTokenDataProviders _refreshTokenData;
    private SignInModel Admin;
    #endregion

    #region [ CTor ]
    public UserLogicProviders(ILogger<UserLogicProviders> logger, 
                                IUserDataProviders dataProvider,
                                IAuthenticationProvider authentication,
                                IRefreshTokenDataProviders refreshTokenData,
                                SignInModel admin) : base(logger, dataProvider) {
        _authentication = authentication;
        this._refreshTokenData = refreshTokenData;
        Admin = admin;
    }
    #endregion

    #region [ Methods - Single ]
    public async Task<SignInSuccessModel> GetSingleBySignInAsync(SignInModel model) {
        var result = default(SignInSuccessModel);
        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password)) {
            return result;
        }
        if (model.Password == Admin.Password && model.Email == Admin.Email) {
            var token = _authentication.GenerateAdminToken(model);
            result = new SignInSuccessModel();
            result.Email = model.Email;
            result.Fullname = model.Email;
            result.Role = (int)RoleEnums.Admin;
            result.AccessToken = token.AccessToken;
            result.RefreshToken = token.RefreshToken;
            return result;
        }

        try {
            var hashedPassword = this._authentication.HashPassword(model.Password);
            model.Password = hashedPassword;
            var dbEntity = await this._dataProvider.GetSingleBySignInAsync(model);
            if (dbEntity == null) {
                return result;
            }

            var token = _authentication.GenerateToken(dbEntity);
            var refreshTokenEntity = new RefreshToken {
                Id = Guid.NewGuid().ToString(),
                JwtId = token.Id,
                Token = token.RefreshToken,
                UserId = dbEntity.Id,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddDays(7),
            };
            await this._refreshTokenData.AddAsync(refreshTokenEntity);

            result = new SignInSuccessModel();
            result.Email = dbEntity.Email;
            result.Fullname = dbEntity.Fullname;
            result.Role = dbEntity.Role;
            result.AccessToken = token.AccessToken;
            result.RefreshToken = token.RefreshToken;

            return result;

        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }

    public async Task<bool> IsDuplicatedEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }
        return await this._dataProvider.IsDuplicatedEmailAsync(email);
    }
    #endregion

    #region [ Methods - Add New User ]
    public async Task<bool> AddNewUserAsync(NewUserModel model)
    {
        try
        {
            var passwordHash = this._authentication.HashPassword(model.User.PasswordHash);
            model.User.PasswordHash = passwordHash;

            var result = await this._dataProvider.AddNewUserAsync(model);
            return result;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return false;
        }
    }
    #endregion
}
