using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.Models
{
    public class RoleAssigns
    {
        public RoleAssigns()
        {
            RoleAssignID = 0;
            RoleID = 0;
            UserID = 0;
        }
        [Key]
        public int RoleAssignID { get; set;}
        public int RoleID { get; set;}
        public int UserID { get; set;}
    }
}
