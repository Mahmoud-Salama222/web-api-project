using AutoMapper;
using Core.Entity;
using Core.Repositry;
using Core.specfcation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Repositry.Data.Configration;
using WebApplicationApi.Dto;
using WebApplicationApi.Error;
using WebApplicationApi.Helper;

namespace WebApplicationApi.Controllers
{

    public class proudctController : ApibaseController
    {
        private readonly IGenricrepositry<Proudct> Proudctrepo;
        private readonly IMapper mapper;
        private readonly IGenricrepositry<proudctbrand> brandrepo;
        private readonly IGenricrepositry<proudcttype> typerepo;

        public proudctController(IGenricrepositry<Proudct> proudctrepo,IMapper mapper, IGenricrepositry<proudctbrand> brandrepo, IGenricrepositry<proudcttype> typerepo)
        {
            Proudctrepo = proudctrepo;
            this.mapper = mapper;
            this.brandrepo = brandrepo;
            this.typerepo = typerepo;
        }

        ///[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]//use jwt on this  endpoint
        [HttpGet]
        public async Task<ActionResult<pagination<ProudcttoDtocs>>> Getproudcts([FromQuery]proudctspecprams proudctspec)
        {
            var spec=new proudctspecfiction(proudctspec); //creat include wihout certial
            var proudcts = await Proudctrepo.GetallwithspecAsync(spec);//make query
            var date=mapper.Map<IReadOnlyList<Proudct>, IReadOnlyList<ProudcttoDtocs>>(proudcts);
            var countspec = new proudctwithfliteringforcountspecfiction(proudctspec);
            var count=await Proudctrepo.GetcountwithspecspecAsync(countspec);
            //var proudcts = await Proudctrepo.GetAllAsync();
            return Ok(new pagination<ProudcttoDtocs>(proudctspec.pageindex,proudctspec.Pagesize,count,date));

        }

        [ProducesResponseType(typeof(ProudcttoDtocs),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Apierrorresponce), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProudcttoDtocs>> Getproudct(int id)
        {
            var spec = new proudctspecfiction(id);

            var proudcts = await Proudctrepo.GetbyidspecAsync(spec);
            if(proudcts is null) { return NotFound(new Apierrorresponce(404)); }
            var mapedproudct = mapper.Map<Proudct, ProudcttoDtocs>(proudcts);

            return Ok(mapedproudct);

        }
        [HttpGet("brand")]
        public async Task<ActionResult<IReadOnlyList<proudctbrand>>> Getallbrand()
        {
           var brands= await brandrepo.GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("type")]
        public async Task<ActionResult<IEnumerable<proudctbrand>>> Getalltype()
        {
            var types=await typerepo.GetAllAsync();
            return Ok(types);
        }

    }
}
