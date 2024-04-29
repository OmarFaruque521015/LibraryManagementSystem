using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ViewModels
{
    public class UserVM
    {
        public UserVM()
        {
            UserName = "";
            Password = "";
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
