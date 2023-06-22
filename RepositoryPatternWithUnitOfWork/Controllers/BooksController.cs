using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUnitOfWork.Core.Consts;
using RepositoryPatternWithUnitOfWork.Core.Interfaces;
using RepositoryPatternWithUnitOfWork.Core.Models;

namespace RepositoryPatternWithUnitOfWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBaseRepository<Book> bookRepository;
        public BooksController(IBaseRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(bookRepository.GetByIdAsync(1));
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(bookRepository.GetAll());
        }
        [HttpGet("GetByName")]
        public IActionResult GetByName()
        {
            return Ok(bookRepository.Find(x => x.Title == "Book 1", new[] {"Authors"} ));
        }
        [HttpGet("GetAllWithAuthors")]
        public IActionResult GetAllWithAuthors()
        {
            return Ok(bookRepository.FindAll(x => x.Title.Contains( "Book 1"), new[] { "Authors" }));
        }
        [HttpGet("GetOrdered")]
        public IActionResult GetOrdered()
        {
            return Ok(bookRepository.FindAll(x => x.Title.Contains("Book 1"), null, 0, x => x.Title, OrderBy.Descending));
        }
        [HttpPost("AddOne")]
        public IActionResult AddOne()
        {
            return Ok(bookRepository.Add(new Book { Title = "Book 3" }));
        }
        [HttpPost("AddRange")]
        public IActionResult AddRange()
        {
            return Ok(bookRepository.AddRange(new[] { new Book { Title = "Book 4" }, new Book { Title = "Book 5" } }));
        }
    }
}
