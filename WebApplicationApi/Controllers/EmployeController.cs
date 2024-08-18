using Core.Entity;
using Core.Repositry;
using Core.specfcation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeController : ApibaseController
    {
        private readonly IGenricrepositry<Employe> employe;

        public EmployeController(IGenricrepositry<Employe> employe)
        {
            this.employe = employe;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employe>>> GetEmploye() {
            var spec = new Employespecfcaioncs();
            var employes = employe.GetallwithspecAsync(spec);

            return Ok(employes);


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Employe>>> GetEmployebyid()
        {
            var spec = new Employespecfcaioncs();
            var employes = employe.GetbyidspecAsync(spec);

            return Ok(employe);


        }

    }
}


