using Products.Api.Data;
using Microsoft.EntityFrameworkCore;
using Products.Api.Configuration;
using Products.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDbContext<DbContext, ProductsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

services.AddCustomValidation();
services.AddRepositories();
services.AddCustomServices();
services.AddCustomSwaggerConfiguration(builder.Configuration);
services.AddCustomAuthentication(builder.Configuration);
services.AddAuthorization();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//TODO Добавить работу с медиа (сохранение в облако), GC, Распределение, cdn, и.т.д