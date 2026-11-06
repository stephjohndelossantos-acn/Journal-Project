using Journal.Data;
using JournalAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

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

            //DATABASE service DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("LocalSqlConnection"),
                    sql => sql.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));

            // other services
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

            builder.Services.AddEndpointsApiExplorer();

            //Swagger will now give detailed description on response
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Journal Catalog API",
                    Version = "v1",
                    Description = "Browse and manage the journals and product types of the Nama Pacis catalog."
                });

                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "X-Api-Key",
                    Description = "Needed only for POST, PUT and DELETE."
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("ApiKey", document)] = []
                });
            });


            var app = builder.Build();

            //retries check sql on DbContext
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                for (var attempt = 1; ; attempt++)
                {
                    try { db.Database.Migrate(); break; }
                    catch when (attempt < 10) { Thread.Sleep(TimeSpan.FromSeconds(5)); }
                }

                if (app.Environment.IsDevelopment())
                    JournalSeed.SeedAsync(db).GetAwaiter().GetResult();
            }
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
