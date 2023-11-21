namespace SendOTP.RequestModel
{
    public class AddUserModelRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

    }
}
