using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Service.Interfaces;
using Advanced_CSharp.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.API
{
    public static class ServiceExtensionConfigurations
    {


        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            _ = services.AddHttpContextAccessor();
            _ = services.AddScoped<IUnitWork, UnitWork>();
            _ = services.AddScoped<IProductService, ProductService>();
            _ = services.AddScoped<IAuthenticationService, AuthenticationService>();
            _ = services.AddScoped<IApplicationService, ApplicationService>();
            _ = services.AddScoped<IUserService, UserService>();
            _ = services.AddScoped<IRoleService, RoleService>();
            _ = services.AddScoped<IUserRoleService, UserRoleService>();
            _ = services.AddScoped<IJwtService, JwtService>();



        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            _ = services.AddCors(options =>
                    {
                        options.AddPolicy("CorsPolicy", builder =>
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

                    });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _ = services.AddDbContext<AdvancedCSharpDbContext>(opts =>
                    opts.UseSqlServer(connectionString));
        }






    }
}
