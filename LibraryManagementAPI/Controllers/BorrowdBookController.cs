using LibraryManagement.BLL.IRepositories;
using LibraryManagement.Models.Models;
using LibraryManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowdBookController : ControllerBase
    {
        private readonly IRepository<BorrowdBooks> _borrowdBooksRepository;
        public BorrowdBookController(IRepository<BorrowdBooks> borrowdBooksRepository)
        {
            _borrowdBooksRepository = borrowdBooksRepository;
        }

        // GET: api/<BorrowdBookController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _borrowdBooksRepository.GetAllAsync();
                return Ok(data);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error: " + exc.Message);
            }
        }

        // GET api/<BorrowdBookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var AthrId = await _borrowdBooksRepository.GetByIdAsync(id);
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

        // POST api/<BorrowdBookController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BorrowdBookVM borrowdBookVM)
        {
            try
            {
                var borrowdBooksEntity = new BorrowdBooks()
                {
                    MemberID = borrowdBookVM.MemberID,
                    BookID = borrowdBookVM.BookID,
                    ReturnDate = borrowdBookVM.ReturnDate,
                    Status = borrowdBookVM.Status
                };
                var createBorrowedBook = await _borrowdBooksRepository.AddAsync(borrowdBooksEntity);
                return Ok(createBorrowedBook);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // PUT api/<BorrowdBookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BorrowdBookVM borrowdBookVM)
        {
            try
            {
                var borrowdBooksEntity = await _borrowdBooksRepository.GetByIdAsync(id);
                if (borrowdBooksEntity == null)
                {
                    return NotFound();
                }

                borrowdBooksEntity.MemberID = borrowdBookVM.MemberID;
                borrowdBooksEntity.BookID = borrowdBookVM.BookID;
                borrowdBooksEntity.ReturnDate = borrowdBookVM.ReturnDate;
                borrowdBooksEntity.Status = borrowdBookVM.Status;

                await _borrowdBooksRepository.UpdateAsync(borrowdBooksEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // DELETE api/<BorrowdBookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var borrowdBooksEntity = await _borrowdBooksRepository.GetByIdAsync(id);
                if (borrowdBooksEntity == null)
                {
                    return NotFound();
                }

                await _borrowdBooksRepository.DeleteAsync(borrowdBooksEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }
    }
}
