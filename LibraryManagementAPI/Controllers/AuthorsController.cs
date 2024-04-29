using LibraryManagement.BLL.IRepositories;
using LibraryManagement.Models.Models;
using LibraryManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
 
namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IRepository<Authors> _authorsRepository;
        public AuthorsController(IRepository<Authors> authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _authorsRepository.GetAllAsync();
                return Ok(data);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error: " + exc.Message);
            }
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var AthrId = await _authorsRepository.GetByIdAsync(id);
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

        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthorVM authorVM)
        {
            try
            {
                var authorsEntity = new Authors()
                {
                    AuthorName = authorVM.AuthorName,
                    AuthorBio = authorVM.AuthorBio
                };
                var createAuthor = await _authorsRepository.AddAsync(authorsEntity);
                return Ok(createAuthor);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AuthorVM authorVM)
        {
            try
            {
                var authorsEntity = await _authorsRepository.GetByIdAsync(id);
                if (authorsEntity==null)
                {
                    return NotFound();
                }
                authorsEntity.AuthorName = authorVM.AuthorName; 
                authorsEntity.AuthorBio=authorVM.AuthorBio;

                await _authorsRepository.UpdateAsync(authorsEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message); 
            }
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var authorsEntity = await _authorsRepository.GetByIdAsync(id);
                if (authorsEntity == null)
                {
                    return NotFound();
                }
                 
                await _authorsRepository.DeleteAsync(authorsEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }
    }
}
