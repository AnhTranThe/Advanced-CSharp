using Advanced_CSharp.DTO.Requests.Role;
using Advanced_CSharp.DTO.Requests.User;
using Advanced_CSharp.DTO.Requests.UserRole;
using Advanced_CSharp.DTO.Responses.Role;
using Advanced_CSharp.DTO.Responses.User;
using Advanced_CSharp.DTO.Responses.UserRole;
using Advanced_CSharp.Service.Helper;
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
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;




        public JwtService(IOptions<AppSettings> appSettings, IUserService userService, IUserRoleService userRoleService, IRoleService roleService)
        {

            _appSettings = appSettings.Value;
            _userService = userService;
            _userRoleService = userRoleService;
            _roleService = roleService;
        }

        public async Task<string> GenerateToken(UserResponse userResponse)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            string jwtToken = string.Empty;

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_appSettings.Secret));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // Check if the response is not null
            if (userResponse != null)
            {


                // Retrieve user details using the provided userResponse
                UserGetByIdRequest userGetByIdRequest = new()
                {
                    Id = userResponse.Id
                };

                UserGetByIdResponse response = await _userService.GetByIdAsync(userGetByIdRequest);



                // Check if the response is not null
                if (response != null)
                {
                    UserRoleGetByIdRequest userRoleGetByIdRequest = new()
                    {
                        UserId = response.userResponse.Id

                    };

                    UserRoleGetByIdResponse userRoleGetByIdResponse = await _userRoleService.GetByIdAsync(userRoleGetByIdRequest);
                    if (userRoleGetByIdResponse != null)

                    {
                        RoleGetByIdRequest roleGetByIdRequest = new()
                        {
                            Id = userRoleGetByIdResponse.userRoleResponse.RoleId
                        };
                        RoleGetByIdResponse roleGetByIdResponse = await _roleService.GetByIdAsync(roleGetByIdRequest);

                        if (roleGetByIdResponse != null)
                        {

                            SecurityTokenDescriptor tokenDescriptor = new()
                            {
                                Subject = new ClaimsIdentity(new[]
                                {
                                    new Claim("id",response.userResponse.Id.ToString() ),
                                    new Claim(JwtRegisteredClaimNames.Sub, response.userResponse.UserName),
                                    new Claim(JwtRegisteredClaimNames.Email, response.userResponse.Email),
                                    new Claim("Role", roleGetByIdResponse.roleResponse.RoleName),
                                    new Claim(JwtRegisteredClaimNames.Jti,
                                    Guid.NewGuid().ToString())
                                }),
                                Expires = DateTime.UtcNow.AddMinutes(30),
                                SigningCredentials = credentials
                            };



                            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
                            jwtToken = tokenHandler.WriteToken(token);
                            _ = tokenHandler.WriteToken(token);

                        }


                    }

                }
            }

            return jwtToken;
        }

        public Guid? ValidateJwtToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new();
                SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_appSettings.Secret));
                _ = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = key,
                    // set clockskew to zero so tokens expire exactly at token expiration time.
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                Guid userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                // attach user to context on successful jwt validation

                return userId;

            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
