using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Service.Interfaces;
using Advanced_CSharp.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Advanced_CSharp.Test
{
    public static class DomainServiceCollectionExtensions
    {

        /// <summary>
        /// SetupUserService
        /// </summary>
        /// <returns></returns>
        public static IUserService SetupUserService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IUserService? userService = serviceProvider.GetService<IUserService>() ??
                            throw new InvalidOperationException("IUserService not registered.");

            return userService;
        }

        /// <summary>
        /// SetupRoleService
        /// </summary>
        /// <returns></returns>
        public static IRoleService SetupRoleService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IRoleService? roleService = serviceProvider.GetService<IRoleService>() ??
                            throw new InvalidOperationException("IRoleService not registered.");

            return roleService;
        }

        /// <summary>
        /// SetupUserRoleService
        /// </summary>
        /// <returns></returns>
        public static IUserRoleService SetupUserRoleService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IUserRoleService? userRoleService = serviceProvider.GetService<IUserRoleService>() ??
                            throw new InvalidOperationException("IUserRoleService not registered.");

            return userRoleService;
        }



        /// <summary>
        /// SetupProductService
        /// </summary>
        /// <returns></returns>
        public static IProductService SetupProductService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IProductService? productService = serviceProvider.GetService<IProductService>() ??
                                throw new InvalidOperationException("IProductService not registered.");
            return productService;
        }

        /// <summary>
        /// SetupCartService
        /// </summary>
        /// <returns></returns>
        public static ICartService SetupCartService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            ICartService? cartService = serviceProvider.GetService<ICartService>() ??
                            throw new InvalidOperationException("ICartService not registered.");

            return cartService;
        }

        /// <summary>
        /// SetupCartDetailService
        /// </summary>
        /// <returns></returns>
        public static ICartDetailService SetupCartDetailService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            ICartDetailService? cartDetailService = serviceProvider.GetService<ICartDetailService>() ??
                            throw new InvalidOperationException("ICartDetailService not registered.");

            return cartDetailService;
        }


        /// <summary>
        /// SetupOrderService
        /// </summary>
        /// <returns></returns>
        public static IOrderService SetupOrderService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IOrderService? orderService = serviceProvider.GetService<IOrderService>() ??
                                throw new InvalidOperationException("IOrderService not registered.");

            return orderService;
        }


        /// <summary>
        /// SetupOrderDetailService
        /// </summary>
        /// <returns></returns>
        public static IOrderDetailService SetupOrderDetailService()
        {
            ServiceCollection services = new();
            IConfiguration configuration = new ConfigurationBuilder().Build();
            _ = services.AddDomainServices(configuration);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IOrderDetailService? orderDetailService = serviceProvider.GetService<IOrderDetailService>() ??
                            throw new InvalidOperationException("IOrderDetailService not registered.");

            return orderDetailService;
        }


        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = "Data Source=trantheanh.duckdns.org,1433; Initial Catalog=AdvancedCSharp;User ID=tester;Password=Anh@545658; TrustServerCertificate=true";

            _ = services.AddDbContext<AdvancedCSharpDbContext>(opts =>
                    opts.UseSqlServer(connectionString));

            // Other services registered here
            _ = services.AddHttpContextAccessor();
            _ = services.AddScoped<IUnitWork, UnitWork>();
            _ = services.AddScoped<IProductService, ProductService>();
            _ = services.AddScoped<IAuthenticationService, AuthenticationService>();
            _ = services.AddScoped<IApplicationService, ApplicationService>();
            _ = services.AddScoped<IUserService, UserService>();
            _ = services.AddScoped<IRoleService, RoleService>();
            _ = services.AddScoped<IUserRoleService, UserRoleService>();
            _ = services.AddScoped<ICartService, CartService>();
            _ = services.AddScoped<ICartDetailService, CartDetailService>();
            _ = services.AddScoped<IOrderService, OrderService>();
            _ = services.AddScoped<IOrderDetailService, OrderDetailService>();

            return services;
        }
    }
}
