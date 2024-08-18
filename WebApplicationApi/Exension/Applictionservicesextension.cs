using Core.Repositry;
using Microsoft.AspNetCore.Mvc;
using Repositry;
using WebApplicationApi.Error;
using WebApplicationApi.Helper;

namespace WebApplicationApi.Exension
{
    public static class Applictionservicesextension
    {
        public static  IServiceCollection Addapplictionservixes(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenricrepositry<>), typeof(Genricrepositry<>));
            Services.AddAutoMapper(typeof(Mappingprofilecs));
            Services.AddScoped(typeof(Ibasketrepositry), typeof(Baskerrepositry));
            Services.Configure<ApiBehaviorOptions>(option =>
            option.InvalidModelStateResponseFactory = (ActionContext) =>
            {
                var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                .SelectMany(p => p.Value.Errors).Select(e => e.ErrorMessage);
                var valdtion = new Apivaldateerrorresposecs()
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(valdtion);
            });
            return Services;
        }

    }
}
