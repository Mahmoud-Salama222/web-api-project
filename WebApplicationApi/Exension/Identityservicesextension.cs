using Core.Entity.identity;
using Core.Identity;
using Core.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.Text;

namespace WebApplicationApi.Exension
{
    public static class Identityservicesextension
    {
        public static IServiceCollection Addidntityservices(this IServiceCollection Services ,IConfiguration configuration)
        {
            Services.AddScoped<ITokenservices, Tokenservices>();
            Services.AddIdentity<Appuser, IdentityRole>(options =>            
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
               

            }).AddEntityFrameworkStores<AppIdentityDbcontext>();

            Services.AddAuthentication(option=> { /// make authontiction for jwt
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            })  
            

                .AddJwtBearer(options=>
                {options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Validissuer"],
                    ValidateAudience = true,
                    ValidAudience= configuration["Jwt:ValidAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

                };
          });
            return Services;
        }
    }
}
