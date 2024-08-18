using Core.Entity;
using Core.Repositry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplicationApi.Error;

namespace WebApplicationApi.Controllers
{

    public class BasketController : ApibaseController
    {
        private readonly Ibasketrepositry Basketrepositry;
        public BasketController(Ibasketrepositry basketrepositry)
        {
            Basketrepositry = basketrepositry;
        }

        [HttpGet("{id}")]
       public async Task<ActionResult<customerBaket>> Getcustombasket(string id) { 
        var basket= await Basketrepositry.GetBasketAsync(id);
            return basket is null?new customerBaket(id) : basket;
        }
        [HttpPost()]

        public async Task<ActionResult<customerBaket>> updatecustombasket(customerBaket obj)
        {
            var basket = await Basketrepositry.updatebasketasync(obj);
            if (basket is null) return BadRequest( new Apierrorresponce(400));
            return Ok(basket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> deletcustombasket(string id)
        {
            return await Basketrepositry.Deletbasketasync(id);
            
        }

    }
}
