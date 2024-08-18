using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.InteropServices;
using WebApplicationApi.Error;
using System.Text.Json;
using static System.Text.Json.JsonSerializer;

namespace WebApplicationApi.Midelware
{
    public class Exceptionmideleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<Exceptionmideleware> logger;
        private readonly IHostEnvironment env;

        public Exceptionmideleware(RequestDelegate next,ILogger<Exceptionmideleware> logger,IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async  Task  Invoke(HttpContext context)
        {

            try
            {
                await next.Invoke(context);
            }

            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);

                context.Response.ContentType = "appliction/json";
                context.Response.StatusCode=(int) HttpStatusCode.InternalServerError;

                //var option = new JsonSerializerOptions()
                //{
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase  
                //};
                var response = env.IsDevelopment() ?
                        new ApiExeptionresponce((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                        :
                        new ApiExeptionresponce((int)HttpStatusCode.InternalServerError,ex.Message);


                var option=new JsonSerializerOptions() { PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
                var json = Serialize(response);

                await context.Response.WriteAsync(json);    
                    
                    
            }

        }
    }
}
