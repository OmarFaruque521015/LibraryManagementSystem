using LibraryManagement.BLL.IRepositories;
using LibraryManagement.Models.Models;
using LibraryManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
 
namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Books> _booksRepository;
        public BooksController(IRepository<Books> booksRepository)
        {
            _booksRepository = booksRepository;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _booksRepository.GetAllAsync();
                return Ok(data);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error: " + exc.Message);
            }
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var BookId = await _booksRepository.GetByIdAsync(id);
                if (BookId == null)
                {
                    return NotFound();
                }
                return Ok(BookId);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal Server error" + exc.Message);
            }
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookVM bookVM)
        {
            try
            {
                var booksEntity = new Books()
                {
                    Title = bookVM.Title,
                    ISBN = bookVM.ISBN,
                    AuthorID=bookVM.AuthorID,
                    PublishedDate=bookVM.PublishedDate,
                    AvailableCopies= bookVM.AvailableCopies,
                    TotalCopies= bookVM.TotalCopies
                };
                var createBook = await _booksRepository.AddAsync(booksEntity);
                return Ok(createBook);
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BookVM bookVM)
        {
            try
            {
                var booksEntity = await _booksRepository.GetByIdAsync(id);
                if (booksEntity==null)
                {
                    return NotFound();
                }

                booksEntity.Title = bookVM.Title;
                booksEntity.ISBN = bookVM.ISBN;
                booksEntity.AuthorID = bookVM.AuthorID;
                booksEntity.PublishedDate = bookVM.PublishedDate;
                booksEntity.AvailableCopies = bookVM.AvailableCopies;
                booksEntity.TotalCopies = bookVM.TotalCopies;

                await _booksRepository.UpdateAsync(booksEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message); 
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var booksEntity = await _booksRepository.GetByIdAsync(id);
                if (booksEntity == null)
                {
                    return NotFound();
                }
                 
                await _booksRepository.DeleteAsync(booksEntity);
                return NoContent();
            }
            catch (Exception exc)
            {
                return StatusCode(500, "Internal server error" + exc.Message);
            }
        }
    }
}
