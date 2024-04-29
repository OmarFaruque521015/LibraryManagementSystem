using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models.Models;

public class Users
{
    public Users()
    {
        UserID = 0;
        UserName = "";
        Password = "";
    }
    [Key]
    public int UserID { get; set; } 
    public string UserName { get; set; }
    public string Password { get; set; }
}
