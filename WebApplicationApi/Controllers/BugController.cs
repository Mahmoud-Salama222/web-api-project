using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositry.Data;
using WebApplicationApi.Error;

namespace WebApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ApibaseController
    {
        private readonly Storercontext context;

        public BugController(Storercontext context)
        {
            this.context = context;
        }
        [HttpGet("Notfound")]
        public ActionResult Getnotfound()
        {

            var proudct = context.Proudcts.Find(100);
            if (proudct == null) { return NotFound(new Apierrorresponce(404)); }
            return Ok(proudct);

        }
        [HttpGet("servererror")]

        public ActionResult Getserverrror()
        {

            var proudct = context.Proudcts.Find(100);
            var proudctsreturn = proudct.ToString();
         
            return Ok(proudctsreturn);

        }
        [HttpGet("Badrequest")]

        public ActionResult GetBadrequest()
        {

            return BadRequest(new Apierrorresponce(400));

        }
        [HttpGet("Badrequest/{id}")]

        public ActionResult GetBadrequest(int id)
        {

            return Ok(BadRequest());

        }

        

    }
}
