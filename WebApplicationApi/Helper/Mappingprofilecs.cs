using AutoMapper;
using Core.Entity;
using WebApplicationApi.Dto;

namespace WebApplicationApi.Helper
{
    public class Mappingprofilecs:Profile
    {
        public Mappingprofilecs()
        {
            CreateMap<Proudct, ProudcttoDtocs>().ForMember(mo=>mo.Proudctbrand, m=>m.MapFrom(p=>p.Proudctbrand.name))
            .ForMember(mo => mo.Proudcttype, m => m.MapFrom(p => p.Proudcttype.name))
            .ForMember(po=>po.PictureUrl,o=>o.MapFrom<Proudctpictureresolve>());
        }
    }
}
