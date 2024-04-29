using LibraryManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.BLL.IRepositories
{
    public interface IAuthorRepository
    {
        Task<AuthorReturnVM> AuthenticateUserAsync(UserVM userVM);
    }
}
