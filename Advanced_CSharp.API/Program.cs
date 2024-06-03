using Advanced_CSharp.API;
using Advanced_CSharp.Service.Helper;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfigurationRoot configuration = new ConfigurationBuilder()
     .SetBasePath(AppContext.BaseDirectory)
     .AddJsonFile("appsettings.json")
     .Build();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<AppSettings>(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureAddSwagger();
builder.Services.ConfigureAuthentication(configuration);
builder.Services.AddAuthorization();
// custom service add config


builder.Services.ConfigureCors();
builder.Services.AddLog4net(configuration);
builder.Services.ConfigureSqlContext(configuration);
builder.Services.ConfigureServiceManager();
builder.Services.AddControllers();
// configure strongly typed settings object

WebApplication app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    _ = app.UseSwagger();
//    _ = app.UseSwaggerUI();
//}
_ = app.UseSwagger();
_ = app.UseSwaggerUI();
_ = app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseStaticFiles();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
