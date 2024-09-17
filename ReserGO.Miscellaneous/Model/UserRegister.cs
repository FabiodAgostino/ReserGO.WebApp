using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserGO.Miscellaneous.Model
{
    public class UserRegister
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime? DateOfBirth { get; set; } = DateTime.Today;
        public string CityOfBirth { get; set; } = "";
        public string Email { get; set; } = "";
        public string? PhoneNumber { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";
        public string? Salt { get; set; }
        public List<int>? Permissions { get; set; }
    }
}



