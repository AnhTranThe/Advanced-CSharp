using Advanced_CSharp.API;
using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Service.Authorization;
using Advanced_CSharp.Service.Seeding;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
// custom service add config
builder.Services.ConfigureCors();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureServiceManager();
builder.Services.AddControllers();
// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


WebApplication app = builder.Build();

await SeedDatabase();


// Dependency Injection:
async Task SeedDatabase()
{
    using IServiceScope scope = app.Services.CreateScope();
    AdvancedCSharpDbContext scopedContext = scope.ServiceProvider.GetRequiredService<AdvancedCSharpDbContext>();
    await DbInitializer.Initialize(scopedContext);

}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseStaticFiles();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
