using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.ViewModels
{
    public class BorrowdBookVM
    {
        public BorrowdBookVM()
        {
            MemberID = 0;
            BookID = 0;
            ReturnDate = DateTime.Now;
            Status = "";
        }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; }
    }
}
