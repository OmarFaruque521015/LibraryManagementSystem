using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ViewModels
{
    public class RoleVM
    {
        public RoleVM()
        {
            RoleName = string.Empty;
            ActiveStatus = true;
        }
        public string RoleName { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
