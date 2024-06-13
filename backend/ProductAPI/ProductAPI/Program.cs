using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProductAPI.Models.Config;
using ProductAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection(nameof(MongoDBSettings)));

builder.Services.AddSingleton<IMongoDBSettings>(sp =>
    (IMongoDBSettings)sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

builder.Services.AddSingleton<ProductService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", p =>
    {
        p.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Cors");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
