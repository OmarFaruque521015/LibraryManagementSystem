using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.Models
{
    public class Roles
    {
        public Roles()
        {
            RoleID = 0;
            RoleName = string.Empty;
            ActiveStatus = true;
        }
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
