using Core.Entity.identity;
using Core.Identity;
using Core.Repositry;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositry;
using Repositry.Data;
using Repositry.Identity;
using StackExchange.Redis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using WebApplicationApi.Error;
using WebApplicationApi.Exension;
using WebApplicationApi.Helper;
using WebApplicationApi.Midelware;

namespace WebApplicationApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //ubdatedatebass
            // Add services to the container.

            builder.Services.AddControllers();

            //--database
            builder.Services.AddDbContext<Storercontext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddDbContext<AppIdentityDbcontext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var conection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(conection);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            ////-------------------------------------------------------extension services
            builder.Services.Addapplictionservixes();
            builder.Services.Addidntityservices(builder.Configuration);

            builder.Services.Addswaggerservies();





            var app = builder.Build();

            #region //update database
            var scope = app.Services.CreateScope();

            var serviece = scope.ServiceProvider;
            //LoggerFactory
            var LoggerFactory = serviece.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcotext = serviece.GetRequiredService<Storercontext>(); //make for database1
                await dbcotext.Database.MigrateAsync();
                await storecontextseed.SeedAsync(dbcotext);
                var identitydbcontext = serviece.GetRequiredService<AppIdentityDbcontext>();//make for database1
                await identitydbcontext.Database.MigrateAsync();

                var usermanger = serviece.GetRequiredService<UserManager<Appuser>>();//get service that i make
                await AppIdentityDbcontextseed.seeduserasync(usermanger);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an errro ocuer during apply migration");
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.Addapplicionmidelware();

            }
            app.UseCors("AllowSpecificOrigin");

            app.UseMiddleware<Exceptionmideleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseAuthorization();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();



        }
    }



}