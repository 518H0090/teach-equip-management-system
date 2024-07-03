using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Serilog;
using TeachEquipManagement.BLL.IServices;
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

#endregion

#region Register DI

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGraphService, GraphService>();
builder.Services.AddScoped<IPaginationService, PaginationService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
