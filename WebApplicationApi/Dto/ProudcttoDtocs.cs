using Core.Entity;

namespace WebApplicationApi.Dto
{
    public class ProudcttoDtocs
    {
     public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        


        public int ProductBrandId { get; set; }
        public string Proudctbrand { get; set; }

        public int ProductTypeId { get; set; }
        public string Proudcttype { get; set; }
    }
}

