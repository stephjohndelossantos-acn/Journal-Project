using Journal.Data;
using JournalAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace JournalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Testing
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //DATABASE service
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LocalSqlConnection")));

            // other services
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            //try again
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
        }
    }
}
