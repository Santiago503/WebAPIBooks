using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPIBooks.Models;
using WebAPIBooks.Services;

namespace WebAPIBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookServices _bookServices;

        public BookController(BookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetAllBooks()
        {
            try
            {
                var books = await _bookServices.GetAllBooks();
                return Ok(new { Succeeded = true, Message = "Respuesta Exitosa", Data = books });
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { Succeeded = false, Message = ex.Message, Data = "", StatusCode = ex.StatusCode });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBook(int id)
        {
            try
            {
                var book = await _bookServices.GetBook(id);

                return Ok(new { Succeeded = true, Message = "Respuesta Exitosa", Data = book });
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { Succeeded = false, Message = ex.Message, Data = "", StatusCode = ex.StatusCode });
            }
        }

        [HttpGet]
        [Route("Authors/{id}")]
        public async Task<ActionResult<List<Authors>>> GetAuthorsByBook(int id)
        {
            try
            {
                var book = await _bookServices.GetAuthorsByBooks(id);

                return Ok(new { Succeeded = true, Message = "Respuesta Exitosa", Data = book });
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { Succeeded = false, Message = ex.Message, Data = "", StatusCode = ex.StatusCode });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Books>> PostBooks( Books books)
        {
            try
            {
                var book = await _bookServices.PostBook(books);

                return CreatedAtAction("GetAllBooks", book);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { Succeeded = false, Message = ex.Message, Data = "", StatusCode = ex.StatusCode });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Books>> PutBooks(int id, Books books)
        {
            try
            {
                var oneBook = await _bookServices.GetBook(id);//validate if exit

                var book = await _bookServices.PutBook(id, books);

                return NoContent();
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { Succeeded = false, Message = ex.Message, Data = "", StatusCode = ex.StatusCode });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Books>> DeleteStatus(int id)
        {
            try
            {
                var oneBook = await _bookServices.GetBook(id);//validate if exit

                var book = await _bookServices.DeleteBook(id);

                return Accepted();
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { Succeeded = false, Message = ex.Message, Data = "", StatusCode = ex.StatusCode });
            }
        }


    }
}
