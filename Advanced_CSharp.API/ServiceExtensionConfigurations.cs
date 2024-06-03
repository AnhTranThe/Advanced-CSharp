using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Service.Interfaces;
using Advanced_CSharp.Service.Services;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;
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
        public static void AddLog4net(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureLog4Net(configuration.GetSection("Logging:Log4Net"));
            //_ = XmlConfigurator.Configure(new FileInfo("log4net.config"));
            _ = services.AddSingleton(LogManager.GetLogger(typeof(Program)));
        }

        private static void ConfigureLog4Net(IConfigurationSection log4NetConfig)
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            Logger rootLogger = hierarchy.Root;
            rootLogger.Level = log4net.Core.Level.All;

            IConfigurationSection appenders = log4NetConfig.GetSection("Appenders");
            foreach (IConfigurationSection? appenderSection in appenders.GetChildren())
            {
                Type? appenderType = Type.GetType(appenderSection.GetValue<string>("Type"));
                if (appenderType == null)
                {
                    continue;
                }

                AppenderSkeleton? appender = (AppenderSkeleton)Activator.CreateInstance(appenderType);
                if (appender == null)
                {
                    continue;
                }

                if (appender is RollingFileAppender rollingFileAppender)
                {
                    rollingFileAppender.File = appenderSection.GetValue<string>("File");
                    rollingFileAppender.DatePattern = appenderSection.GetValue<string>("DatePattern");
                    rollingFileAppender.StaticLogFileName = appenderSection.GetValue<bool>("StaticLogFileName");
                    rollingFileAppender.AppendToFile = appenderSection.GetValue<bool>("AppendToFile");
                    rollingFileAppender.RollingStyle = (RollingFileAppender.RollingMode)Enum.Parse(typeof(RollingFileAppender.RollingMode), appenderSection.GetValue<string>("RollingStyle"));
                    rollingFileAppender.MaxSizeRollBackups = appenderSection.GetValue<int>("MaxSizeRollBackups");
                    rollingFileAppender.MaximumFileSize = appenderSection.GetValue<string>("MaximumFileSize");

                    Type? layoutType = Type.GetType(appenderSection.GetSection("Layout").GetValue<string>("Type"));
                    if (layoutType != null)
                    {
                        SerializedLayout? layout = (SerializedLayout)Activator.CreateInstance(layoutType);
                        Type? decoratorType = Type.GetType(appenderSection.GetSection("Layout:Decorator").GetValue<string>("Type"));
                        if (decoratorType != null)
                        {
                            log4net.Layout.Decorators.StandardTypesDecorator? decorator = (log4net.Layout.Decorators.StandardTypesDecorator)Activator.CreateInstance(decoratorType);
                            layout.AddDecorator(decorator);
                        }
                        layout.ActivateOptions();
                        rollingFileAppender.Layout = layout;
                    }

                    rollingFileAppender.ActivateOptions();
                }

                hierarchy.Root.AddAppender(appender);
            }

            hierarchy.Configured = true;

            // Log to console that configuration was completed
            ILog logger = LogManager.GetLogger(typeof(Program));
            logger.Info("Log4Net configured successfully.");
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

