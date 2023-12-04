using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.Service.Interfaces;
using Advanced_CSharp.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.API
{
    public static class ServiceExtensionConfigurations
    {


        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            _ = services.AddScoped<IUnitWork, UnitWork>();
            _ = services.AddScoped<IProductService, ProductService>();
            _ = services.AddScoped<IAuthenticationService, AuthenticationService>();
            _ = services.AddScoped<IApplicationService, ApplicationService>();

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

        public static void ConfigureIdentity(this IServiceCollection services)
        {

            _ = services.AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<AdvancedCSharpDbContext>()
        .AddDefaultTokenProviders();

        }


    }
}
