using LibraryManagement.BLL.IRepositories;
using LibraryManagement.Models.Models;
using LibraryManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IRepository<Members> _membersRepository;
        public MembersController(IRepository<Members> membersRepository)
        {
            _membersRepository = membersRepository;
        }

        // GET: api/<MembersController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _membersRepository.GetAllAsync();
                return Ok(data);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error: " + exc.Message);
            }
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var AthrId = await _membersRepository.GetByIdAsync(id);
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

        // POST api/<MembersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MemberVM memberVM)
        {
            try
            {
                var membersEntity = new Members()
                {
                    FirstName = memberVM.FirstName,
                    LastName = memberVM.LastName,
                    Email = memberVM.Email,
                    PhoneNumber = memberVM.PhoneNumber
                };
                var createMember = await _membersRepository.AddAsync(membersEntity);
                return Ok(createMember);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MemberVM memberVM)
        {
            try
            {
                var membersEntity = await _membersRepository.GetByIdAsync(id);
                if (membersEntity == null)
                {
                    return NotFound();
                }
                membersEntity.FirstName = memberVM.FirstName;
                membersEntity.LastName = memberVM.LastName;
                membersEntity.Email = memberVM.Email;
                membersEntity.PhoneNumber = memberVM.PhoneNumber;

                await _membersRepository.UpdateAsync(membersEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var membersEntity = await _membersRepository.GetByIdAsync(id);
                if (membersEntity == null)
                {
                    return NotFound(); 
                }

                await _membersRepository.DeleteAsync(membersEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }
    }
}
