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
    public class RolesController : ControllerBase
    {
        private readonly IRepository<Roles> _rolesRepository;
        public RolesController(IRepository<Roles> rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        // GET: api/<RolesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _rolesRepository.GetAllAsync();
                return Ok(data);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error: " + exc.Message);
            }
        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var AthrId = await _rolesRepository.GetByIdAsync(id);
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

        // POST api/<RolesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleVM orleVM)
        {
            try
            {
                var rolesEntity = new Roles()
                {
                   RoleName = orleVM.RoleName,
                   ActiveStatus = orleVM.ActiveStatus
                };
                var createAuthor = await _rolesRepository.AddAsync(rolesEntity);
                return Ok(createAuthor);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RoleVM roleVM)
        {
            try
            {
                var rolesEntity = await _rolesRepository.GetByIdAsync(id);
                if (rolesEntity == null)
                {
                    return NotFound();
                }
                rolesEntity.RoleName = roleVM.RoleName;
                rolesEntity.ActiveStatus = roleVM.ActiveStatus;

                await _rolesRepository.UpdateAsync(rolesEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var rolesEntity = await _rolesRepository.GetByIdAsync(id);
                if (rolesEntity == null)
                {
                    return NotFound();
                }

                await _rolesRepository.DeleteAsync(rolesEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }
    }
}