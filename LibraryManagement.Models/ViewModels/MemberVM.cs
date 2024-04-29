using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ViewModels
{
    public class MemberVM
    {
        public MemberVM()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            PhoneNumber = "";
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
