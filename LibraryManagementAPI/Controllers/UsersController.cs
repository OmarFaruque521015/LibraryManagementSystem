using LibraryManagement.BLL.IRepositories;
using LibraryManagement.Models.Models;
using LibraryManagement.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController,Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<Users> _usersRepository;
        public UsersController(IRepository<Users> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _usersRepository.GetAllAsync();
                return Ok(data);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error: " + exc.Message);
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var AthrId = await _usersRepository.GetByIdAsync(id);
                if (AthrId == null)
                {
                    return NotFound();
                }
                return Ok(AthrId);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal Server error" + exc.Message);
            }
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserVM userVM)
        {
            try
            {
                var usersEntity = new Users()
                {
                    UserName = userVM.UserName,
                    Password = userVM.Password
                };
                var createAuthor = await _usersRepository.AddAsync(usersEntity);
                return Ok(createAuthor);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserVM userVM)
        {
            try
            {
                var usersEntity = await _usersRepository.GetByIdAsync(id);
                if (usersEntity == null)
                {
                    return NotFound();
                }
                usersEntity.UserName = userVM.UserName;
                usersEntity.Password = userVM.Password;

                await _usersRepository.UpdateAsync(usersEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var usersEntity = await _usersRepository.GetByIdAsync(id);
                if (usersEntity == null)
                {
                    return NotFound();
                }

                await _usersRepository.DeleteAsync(usersEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }
    }
}
