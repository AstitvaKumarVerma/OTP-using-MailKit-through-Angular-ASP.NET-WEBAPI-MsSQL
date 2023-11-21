using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendOTP.Repository;
using SendOTP.RequestModel;

namespace SendOTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailOtpController : ControllerBase
    {
        private readonly IRegister _register;
        public EmailOtpController(IRegister register)
        {
            _register = register;
        }
        [HttpPost]
        [Route("VerifyOtp")]
        public IActionResult VerifyOtp([FromBody] OtpModel model)
        {
            var obj = _register.verifyotp(model);
            return Ok(obj);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(AddUserModelRequest register)
        {
            var data = _register.registeruser(register);
            return Ok(data);
        }
    }
}
