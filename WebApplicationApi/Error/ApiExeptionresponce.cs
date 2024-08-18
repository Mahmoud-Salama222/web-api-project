using WebApplicationApi.Controllers;

namespace WebApplicationApi.Error
{
    public class ApiExeptionresponce : Apierrorresponce
    {
        public string? Details { get; set; }

        public ApiExeptionresponce(int statuescode, string? message=null, string? Details=null) : base(statuescode, message)
        {
            this.Details = Details;
        }

    }
}
