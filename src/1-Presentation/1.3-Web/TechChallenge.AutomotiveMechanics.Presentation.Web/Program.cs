using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechChallenge.AutomotiveMechanics.Crosscutting.Ioc;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;
using static Org.BouncyCastle.Math.EC.ECCurve;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcCore();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AutomotiveMechanics"));
});

builder.Services.AddScoped<IBaseNotification, BaseNotification>();

//builder.Services.AddScoped<ICarService, CarService>();
//builder.Services.AddScoped<ICarRepository, CarRepository>();

builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();

builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IModelRepository, ModelRepository>();

//builder.Services.AddScoped<IOrderService, OrderService>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//builder.Services.AddScoped<IServiceService, ServiceService>();
//builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

AutoMapperConfig.ConfigureMappings(builder.Services);
//builder.Services.AddHttpClient();

//var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("SecretJWT"));


//builder.Services
//                .AddAuthentication(x =>
//                {
//                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//                })
//                .AddJwtBearer(x =>
//                {
//                    x.RequireHttpsMetadata = false;
//                    x.SaveToken = true;
//                    x.TokenValidationParameters = new TokenValidationParameters()
//                    {
//                        ValidateIssuerSigningKey = true,
//                        IssuerSigningKey = new SymmetricSecurityKey(key),
//                        ValidateIssuer = false,
//                        ValidateAudience = false,
//                    };
//                })
//                .AddCookie(x =>
//                {
//                    x.LoginPath = "/User/Login";
//                });



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login";
        options.AccessDeniedPath = "/User/AccessDenied";
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapControllers();
app.MapRazorPages();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
