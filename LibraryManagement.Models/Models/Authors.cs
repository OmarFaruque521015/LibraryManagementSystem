﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models.Models
{
    public class Authors
    {
        public Authors()
        {
            AuthorID = 0;
            AuthorName = "";
            AuthorBio = "";
        }
        [Key]
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string AuthorBio { get; set; }
    }
}
