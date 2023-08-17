using Assignment03.EntityProviders;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Assignment03.LogicProviders;

public class AuthenticationProvider : IAuthenticationProvider
{
    #region [ Fields ]
    public readonly AppSettingModel _appSettingModel;
    #endregion

    #region [ CTor ]
    public AuthenticationProvider(AppSettingModel appSettingModel) {
        this._appSettingModel = appSettingModel;
    }
    #endregion

    #region [ Methods - Generate Token ]
    private string GenerateRefreshToken() {
        var result = string.Empty;
        var random = new byte[32];
        using (var rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(random);
        }

        result = Convert.ToBase64String(random);
        return result;
    }

    public TokenModel GenerateToken(User user) {
        var userRole = string.Empty;
        switch (user.Role) {
            case (int)RoleEnums.Manager:
                userRole = AppUserRole.Manager;
                break;
            case (int)RoleEnums.Admin:
                userRole = AppUserRole.Admin;
                break;
            default:
                userRole = AppUserRole.Employee;
                break;
        }

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettingModel.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", user.Id),

                // roles
                new Claim(ClaimTypes.Role, userRole),
            }),
            Expires = DateTime.UtcNow.AddDays(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
        };
        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var accessToken = jwtTokenHandler.WriteToken(token);
        var refreshToken = GenerateRefreshToken();

        var result = new TokenModel {
            Id = token.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return result;
    }

    public TokenModel GenerateAdminToken(SignInModel admin) {
        var userRole = AppUserRole.Admin;
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettingModel.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                new Claim(JwtRegisteredClaimNames.Email, admin.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                // roles
                new Claim(ClaimTypes.Role, userRole),
            }),
            Expires = DateTime.UtcNow.AddDays(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
        };
        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var accessToken = jwtTokenHandler.WriteToken(token);
        var refreshToken = GenerateRefreshToken();

        var result = new TokenModel {
            Id = token.Id,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return result;
    }
    #endregion

    #region [ Methods - HashPassword ]
    public string HashPassword(string password) {
        var result = string.Empty;
        using (var sha512 = SHA512.Create()) {
            var passwordData = Encoding.UTF8.GetBytes(password);
            var hashValue = sha512.ComputeHash(passwordData);
            foreach (byte x in hashValue) {
                result += string.Format("{0:x2}", x);
            }
        }
        return result;
    }
    #endregion
}
