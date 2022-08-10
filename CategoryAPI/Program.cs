using CategoryAPI.Data;
using CategoryAPI.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//CAP Configuration
builder.Services.AddDbContext<ApiContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddCap(options =>
{
    options.DefaultGroupName = "Product";
    options.UseDashboard();
    options.UseEntityFramework<ApiContext>();
    options.UsePostgreSql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    options.UseRabbitMQ(opt =>
    {
        opt.HostName = builder.Configuration.GetValue<string>("RabbitMqHostName");
        opt.UserName = "admin";
        opt.Password = "admin";
        opt.ExchangeName = "ProductExchange";
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();