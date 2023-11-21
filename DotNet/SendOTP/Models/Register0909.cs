using System;
using System.Collections.Generic;

namespace SendOTP.Models
{
    public partial class Register0909
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsAuthenticated { get; set; }
        public string? Otp { get; set; }
    }
}
