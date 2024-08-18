using AutoMapper;
using Core.Entity;
using WebApplicationApi.Dto;

namespace WebApplicationApi.Helper
{
    public class Proudctpictureresolve:IValueResolver<Proudct, ProudcttoDtocs,string>
    {
        private readonly IConfiguration configuration;

        public Proudctpictureresolve(IConfiguration configuration)
        {
            this.configuration= configuration;


        }
        public string Resolve(Proudct source, ProudcttoDtocs destination, string destMember, ResolutionContext context)
        {

            if(! string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["ApiBaseUrl"]}{ source.PictureUrl}";

            }
            return string.Empty;
        }
    }
}
