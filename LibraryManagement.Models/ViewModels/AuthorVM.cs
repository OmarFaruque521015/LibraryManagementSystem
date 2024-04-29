using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ViewModels
{
    public class AuthorVM
    {
        public AuthorVM()
        {
            AuthorName = "";
            AuthorBio = "";
        }
        public string AuthorName { get; set; }
        public string AuthorBio { get; set; }
    }
}
