using WebApplicationApi.Controllers;

namespace WebApplicationApi.Error
{
    public class Apivaldateerrorresposecs: Apierrorresponce
    {
        public IEnumerable<string> Errors { get; set; }
        public Apivaldateerrorresposecs():base(400)
        {
            Errors=new List<string>();


        }
    }
}
