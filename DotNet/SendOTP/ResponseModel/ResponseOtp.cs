namespace SendOTP.ResponseModel
{
    public class ResponseOtp
    {
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
        public bool verified { get; set; }
    }
}
