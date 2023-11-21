using NETCore.MailKit.Core;
using System.Net.Mail;
using System.Net;
using SendOTP.ResponseModel;
using SendOTP.Models;
using SendOTP.RequestModel;

namespace SendOTP.Repository
{
    public class RegisterRepository : IRegister
    {
        public readonly sdirectdbContext _context;
        private readonly IConfiguration _config;


        public RegisterRepository(IConfiguration config, sdirectdbContext context)
        {
            _context = context;
            _config = config;
        }


        public string generateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }

        public ResponseOtp registeruser(AddUserModelRequest register)
        {
            Register0909 user = new Register0909();
           
            ResponseOtp response = new ResponseOtp();

            var userobj = _context.Register0909s.Where(i => i.Email == register.Email ).ToList();
            if (userobj.Count > 0)
            {
                response.ResponseMessage = "Already";
                response.ResponseCode = 'E';

                return response;
            }

            string otp = generateOtp();

            // Send OTP to the user's email
            ResponseModel.Response otpResponse = SendOtpToEmails(register.Email, otp);

            if (otpResponse.ResponseMessage == "otp not sent to email")
            {
                response.ResponseMessage = "OTP not sent.";
                response.ResponseCode = 'A';
            }
            else
            {
                user.Email = register.Email;
                user.Password = register.Password;
                user.IsAuthenticated = false;
                user.Otp = otp;
                _context.Register0909s.Add(user);
                _context.SaveChanges();
              

                response.ResponseMessage = $"Otp Send to: {user.Email}";
                response.ResponseCode = 'D';
            }

            return response;
        }



        public Response SendOtpToEmails(string Email, string Otp)
        {
            ResponseModel.Response response = new ResponseModel.Response();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                var smtpServer = _config["EmailConfiguration:SmtpServer"];
                var smtpPort = int.Parse(_config["EmailConfiguration:Port"]);
                var smtpUsername = _config["EmailConfiguration:Username"];
                var smtpPassword = _config["EmailConfiguration:Password"];

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    var mail = new MailMessage
                    {
                        From = new MailAddress(smtpUsername),
                        Subject = "OTP Verification",
                        Body = $"Your OTP is: {Otp}",
                        IsBodyHtml = false
                    };
                   
                    //mail.To.Add(Email); var emailHtml = System.IO.File.ReadAllText(_env.WebRootPath + "/amendmentstatus.html");
                    //emailHtml = emailHtml.Replace("{{staff_Name}}", FirstName + " " + LastName);
                    //emailHtml = emailHtml.Replace("{{amendment_status}}", formStatus);
                    //mail.Body = emailHtml;

                    client.Send(mail);

                    response.ResponseMessage = "otp sent to email";

                    return response;
                }
            }
            catch (Exception ex)
            {

                response.ResponseMessage = $"Failed to send OTP email. Error: {ex.Message ?? "Unknown error"}";

                return response;
            }
        }

        public ResponseOtp verifyotp(OtpModel otp)
        {
            Register0909 user = new Register0909();
            ResponseOtp response = new ResponseOtp();

            try
            {
                Register0909 userobj = _context.Register0909s.Where(u => u.Otp == otp.otp ).FirstOrDefault();

                if(userobj != null) 
                {
                    userobj.IsAuthenticated = true;

                    _context.SaveChanges();

                    response.verified = true;

                    response.ResponseMessage = "Successfull";
                    return response;
                }
                else
                {
                    user.IsAuthenticated = false;
                    _context.SaveChanges();
                    response.verified = false;

                    response.ResponseMessage = "Failure";

                    return response;
                }
                
            }
            catch (Exception ex)
            {
                response.verified = false;
                return response;
            }
        }
    }
}
