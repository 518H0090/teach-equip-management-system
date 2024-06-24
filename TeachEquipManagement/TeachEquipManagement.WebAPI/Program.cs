using Microsoft.EntityFrameworkCore;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    builder.Configuration.GetConnectionString(Constants.ConnectionString))
);

# endregion

# region AutoMapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

# endregion

#region Register DI

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
