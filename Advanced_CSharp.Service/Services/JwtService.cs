using Advanced_CSharp.DTO.Requests.User;
using Advanced_CSharp.DTO.Responses.User;
using Advanced_CSharp.Service.Authorization;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Advanced_CSharp.Service.Services
{
    public class JwtService : IJwtService
    {


        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;


        public JwtService(IOptions<AppSettings> appSettings, IUserService userService)
        {

            _appSettings = appSettings.Value;
            _userService = userService;
        }

        public async Task<string> GenerateToken(UserResponse userResponse)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken? token = null;

            // Check if the response is not null
            if (userResponse != null)
            {
                byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                // Retrieve user details using the provided userResponse
                UserGetByIdRequest userGetByIdRequest = new()
                {
                    Id = userResponse.Id
                };

                UserGetByIdResponse response = await _userService.GetByIdAsync(userGetByIdRequest);

                // Check if the response is not null
                if (response != null)
                {
                    SecurityTokenDescriptor tokenDescriptor = new()
                    {
                        Subject = new ClaimsIdentity(new[] { new Claim("id", userResponse.Id.ToString()) }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    // Create the token using the provided descriptor
                    token = tokenHandler.CreateToken(tokenDescriptor);
                }
            }

            return tokenHandler.WriteToken(token);
        }


        public Guid? ValidateToken(string token)
        {
            if (token == null)
            {
                return null;
            }

            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                _ = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                Guid userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

    }
}
