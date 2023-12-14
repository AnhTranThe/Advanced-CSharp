using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Service.Interfaces;
using Advanced_CSharp.Service.Services;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Advanced_CSharp.API
{
    public static class ServiceExtensionConfigurations
    {

        /// <summary>
        /// ConfigureServiceManager
        /// </summary>
        /// <param name="services"></param>
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
            _ = services.AddScoped<ICartService, CartService>();
            _ = services.AddScoped<ICartDetailService, CartDetailService>();
            _ = services.AddScoped<IOrderService, OrderService>();
            _ = services.AddScoped<IOrderDetailService, OrderDetailService>();
            _ = services.AddScoped<IloggingService, LoggingService>();

        }
        /// <summary>
        /// AddLog4net
        /// </summary>
        /// <param name="services"></param>
        public static void AddLog4net(this IServiceCollection services)
        {
            _ = XmlConfigurator.Configure(new FileInfo("log4net.config"));
            _ = services.AddSingleton(LogManager.GetLogger(typeof(Program)));
        }
        /// <summary>
        /// ConfigureCors
        /// </summary>
        /// <param name="services"></param>
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
        /// <summary>
        /// ConfigureSqlContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _ = services.AddDbContext<AdvancedCSharpDbContext>(opts =>
                    opts.UseSqlServer(connectionString));
        }
        /// <summary>
        /// ConfigureAuthentication
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            _ = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["AppSettings:Issuer"],
                    ValidAudience = configuration["AppSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };

            });

        }


        /// <summary>
        /// ConfigureAddSwagger
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAddSwagger(this IServiceCollection services)
        {
            _ = services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
    });
            });
        }



    }



}

