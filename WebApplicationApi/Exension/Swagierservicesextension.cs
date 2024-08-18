namespace WebApplicationApi.Exension
{
    public static  class Swagierservicesextension
    {
        public static IServiceCollection Addswaggerservies(this IServiceCollection Services)
        {

            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;

        }
        public static WebApplication Addapplicionmidelware(this WebApplication app)
        {

            app.UseSwagger();
            app.UseSwaggerUI();

         return app;

        }
    }
}
