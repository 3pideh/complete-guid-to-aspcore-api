using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book.Data.Services;
using my_book.Data.ViewModels;

namespace my_book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost("add-authors")]
        public IActionResult AddBook([FromBody] AuthorVM book)
        {
            _authorsService.AddAuthor(book);
            return Ok();
        }

        [HttpGet("get-all-authors")]
        public IActionResult GetAllauthors()
        {
            var allauthors = _authorsService.GetAllAuthors();
            return Ok(allauthors);
        }

        [HttpGet("get-author-with-books-by-id/{id}")]
        public IActionResult GetAuthorsWithBooks(int id)
        {
            var response = _authorsService.GetAuthorWithBooks(id);
            return Ok(response);
        }

        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _authorsService.GetAuthorById(id);
            return Ok(book);
        }

        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorVM authorVM)
        {
            var updatebook = _authorsService.UpdateAuthorById(id, authorVM);
            return Ok(updatebook);
        }

        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteAuthorById(int id)
        {
            _authorsService.DeleteAuthorById(id);
            return Ok();
        }
    }
}
