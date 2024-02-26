using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using ShopOnline.API.Data;
using ShopOnline.API.Repositories;
using ShopOnline.API.Repositories.Contracts;
using System.Globalization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        //Add connection to database 
        builder.Services.AddDbContext<ShopOnlineDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ShopOnlineConnection"))
        );

        // CORS configuration.
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });


        //Add Scope to register ProductRepository Class

        //ASP.Net Core/ dependency injection (DI) : provided is registering a service in DI container with scope lifetime.
        builder.Services.AddScoped<IProductRepository, ProductRespotory>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // CORS middleware.
        app.UseCors(policy =>
            policy.WithOrigins("http://localhost:7058", "https://localhost:7058")
                  .AllowAnyMethod()
                  .WithHeaders(HeaderNames.ContentType)
        );



        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}