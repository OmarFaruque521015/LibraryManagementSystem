using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ViewModels
{
    public class AuthorReturnVM
    {
        public AuthorReturnVM()
        {
            UserID = 0;
            UserName = string.Empty;
            Token=string.Empty; 
        }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string ?ErrorMessage { get; set;}
    }
}
