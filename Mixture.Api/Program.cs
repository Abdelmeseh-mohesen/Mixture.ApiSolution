using Microsoft.EntityFrameworkCore;
using Mixture.Api.Helper;
using Mixture.Core.Repositery;
using Mixture.Repository;
using Mixture.Repository.Data;
using Mixture.Servise.Abstract;
using Mixture.Servise.Implmention;

namespace Mixture.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //***************Connection STRING **********
            builder.Services.AddDbContext<MixtureContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IPumpReadingService), typeof(PumpReadingService));
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile(typeof(ProfileMapping)));



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Http 
            builder.Services.AddHttpClient();


            var app = builder.Build();
           

            //update database
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();//catch
            try
            {
                var DbContext = services.GetRequiredService<MixtureContext>();
                await DbContext.Database.MigrateAsync();
                
             
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error during Apply migration");

            }

            // Configure the HTTP request pipeline.
          //  if (app.Environment.IsDevelopment())
          //  {
                app.UseSwagger();
                app.UseSwaggerUI();
           // }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
