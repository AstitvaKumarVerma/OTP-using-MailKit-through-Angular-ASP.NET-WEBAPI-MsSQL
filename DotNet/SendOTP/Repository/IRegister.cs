using SendOTP.RequestModel;
using SendOTP.ResponseModel;

namespace SendOTP.Repository
{
    public interface IRegister
    {
        public Response SendOtpToEmails(string Email, string Otp);
        public ResponseOtp verifyotp(OtpModel otp);
        public ResponseOtp registeruser(AddUserModelRequest register);
    }
}
