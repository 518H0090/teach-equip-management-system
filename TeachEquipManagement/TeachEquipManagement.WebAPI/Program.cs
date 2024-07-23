using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.ManageServices;
using TeachEquipManagement.BLL.Services;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.UnitOfWorks;
using TeachEquipManagement.Utilities;
using TeachEquipManagement.Utilities.CustomAttribute;
using TeachEquipManagement.Utilities.OptionPattern;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

# region Register Filter Attribute

builder.Services.AddMvc(options =>
{
    options.Filters.Add<LogFilterAttribute>();
});

#endregion

# region Cors Policy

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    options.AddPolicy(name: "AllowGetPolicy", builder =>
    {
        builder.WithOrigins("*")
            .WithHeaders("*")
            .WithMethods("GET");
    });

    options.AddPolicy(name: "AllowPostPolicy", builder =>
    {
        builder.WithOrigins("*")
            .WithHeaders("*")
            .WithMethods("POST");
    });

    options.AddPolicy(name: "AllowPutPolicy", builder =>
    {
        builder.WithOrigins("*")
            .WithHeaders("*")
            .WithMethods("PUT");
    });

    options.AddPolicy(name: "AllowDeletePolicy", builder =>
    {
        builder.WithOrigins("*")
            .WithHeaders("*")
            .WithMethods("DELETE");
    });
});


# endregion

# region Add DbContext

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString(ConstantValues.ConnectionString))
);

# endregion

# region AutoMapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

# endregion

# region SeriLog

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

# endregion

#region Register Configuration

builder.Services.Configure<AzureAdConfiguration>(builder.Configuration.GetSection("AzureAD"));
builder.Services.Configure<JwtSecretKeyConfiguration>(builder.Configuration.GetSection("Jwt"));

#endregion

# region JwtToken Authentication

var secretKey = builder.Configuration.GetSection("Jwt:SecretKey").Get<string>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = false,
         ValidateAudience = false,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
         ClockSkew = TimeSpan.Zero
     };
 });

#endregion

#region Register DI

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IToolManageService, ToolManageService>();
builder.Services.AddScoped<IInventoryManageService, InventoryManageService>();
builder.Services.AddScoped<IUserManageService, UserManageService>();

builder.Services.AddScoped<IGraphService, GraphService>();

#endregion

# region Graph Service Client

builder.Services.AddSingleton<GraphServiceClient>(serviceProvider =>
{
    var clientId = builder.Configuration["AzureAd:ClientId"];
    var tenantId = builder.Configuration["AzureAd:TenantId"];
    var clientSecret = builder.Configuration["AzureAd:ClientSecret"];

    var scopes = new[] { "https://graph.microsoft.com/.default" };

    if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(clientSecret))
    {
        throw new InvalidOperationException("AzureAd settings are not configured properly.");
    }

    var options = new ClientSecretCredentialOptions
    {
        AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
    };

    var clientSecretCredential = new ClientSecretCredential(
        tenantId, clientId, clientSecret, options);

    var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

    return graphClient;
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
