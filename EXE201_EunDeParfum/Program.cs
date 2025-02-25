using EunDeParfum_Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using EunDeParfum_Repository;
using EXE201_EunDeParfum.AppStarts;
using EunDeParfum_Service.Service.Implement;
using EunDeParfum_Service.Service.Interface;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.Mapper;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.ConfigureSwaggerServices();

builder.Services.ConfigureAuthService(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductsService, ProductsService>();


builder.Services.ServiceContainer(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
