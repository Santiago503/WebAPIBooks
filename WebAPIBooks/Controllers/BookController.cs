using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<Books>>> Get()
        {
            try
            {
                var books = await _bookServices.GetAllBooks();
                return Ok(new { Succeeded = true, Message = "Respuesta Exitosa", Data = books });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Succeeded = false, Message = ex.Message, Data = "" });
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
            catch (Exception ex)
            {
                return BadRequest(new { Succeeded = false, Message = ex.Message, Data = "" });
            }
        }
    }
}
