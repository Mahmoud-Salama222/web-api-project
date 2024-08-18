namespace WebApplicationApi.Error
{
    public class Apierrorresponce
    {
        public int statuecode {  get; set; }
        public string? message { get; set; }
        public Apierrorresponce(int statuecode, string? message=null)
        {
            this.statuecode = statuecode;
            this.message = message ?? getmessagecode(statuecode);
        }
        private string? getmessagecode(int statuecode)
        {
            return statuecode switch
            {
                400 => "bad request",
                404 => "not found",
                500 => "server error",
                401 => "Authorized you are not",
                _=>null

            };
        }

    }
}
