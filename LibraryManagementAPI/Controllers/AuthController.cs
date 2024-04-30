using LibraryManagement.BLL.IRepositories;
using LibraryManagement.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthController(IAuthorRepository authRepository)
        {
            _authorRepository = authRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserVM userVM)
        {
            try
            {
                var authorLogin = await _authorRepository.AuthenticateUserAsync(userVM);

                if (!string.IsNullOrEmpty(authorLogin.ErrorMessage))
                {
                    return BadRequest(new { message = authorLogin.ErrorMessage });
                }
                return Ok(authorLogin);
            }
            catch (Exception x)
            {
                return BadRequest(new { ErrMsg = x.Message, InnerExceptionMsg = x.InnerException != null ? x.InnerException.Message : "", StackTrace = x.StackTrace });
            }
        }

    }
}
