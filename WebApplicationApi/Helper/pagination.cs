using WebApplicationApi.Dto;

namespace WebApplicationApi.Helper
{
    public class pagination<t>
    {
        public int pageindex;
        public int pageSize;
        public int Count;
       // private IReadOnlyList<ProudcttoDtocs> date;

        public IReadOnlyList<t> Data {  get; set; }
        public pagination(int pageindex, int pagesize,int count, IReadOnlyList<t> date)
        {
            this.pageindex = pageindex;
            pageSize = pagesize;
            Data = date;
            Count = count;
        }
    }
}
