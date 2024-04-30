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
    public class RoleAssignsController : ControllerBase
    {
        private readonly IRepository<RoleAssigns> _roleAssignsRepository;
        public RoleAssignsController(IRepository<RoleAssigns> roleAssignsRepository)
        {
            _roleAssignsRepository = roleAssignsRepository;
        }

        // GET: api/<RoleAssignsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _roleAssignsRepository.GetAllAsync();
                return Ok(data);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error: " + exc.Message);
            }
        }

        // GET api/<RoleAssignsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var AthrId = await _roleAssignsRepository.GetByIdAsync(id);
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

        // POST api/<RoleAssignsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleAssignVM roleAssignVM)
        {
            try
            {
                var roleAssignsEntity = new RoleAssigns()
                {
                    UserID = roleAssignVM.UserID,
                    RoleID = roleAssignVM.RoleID
                };
                var createAuthor = await _roleAssignsRepository.AddAsync(roleAssignsEntity);
                return Ok(createAuthor);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // PUT api/<RoleAssignsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RoleAssignVM roleAssignVM)
        {
            try
            {
                var roleAssignsEntity = await _roleAssignsRepository.GetByIdAsync(id);
                if (roleAssignsEntity == null)
                {
                    return NotFound();
                }
                roleAssignsEntity.UserID = roleAssignVM.UserID;
                roleAssignsEntity.RoleID = roleAssignVM.RoleID;

                await _roleAssignsRepository.UpdateAsync(roleAssignsEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // DELETE api/<RoleAssignsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var roleAssignsEntity = await _roleAssignsRepository.GetByIdAsync(id);
                if (roleAssignsEntity == null)
                {
                    return NotFound();
                }

                await _roleAssignsRepository.DeleteAsync(roleAssignsEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }
    }
}
