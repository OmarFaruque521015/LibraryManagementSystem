using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ViewModels
{
    public class RoleAssignVM
    {
        public RoleAssignVM()
        {
            RoleID = 0;
            UserID = 0;
        }
        public int RoleID { get; set; }
        public int UserID { get; set; }
    }
}
