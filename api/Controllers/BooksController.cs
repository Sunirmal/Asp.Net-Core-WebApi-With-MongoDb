using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Entities;
using api.Repositories;
using api.Schemas;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class BooksController: Controller
    {
        private readonly BookRepository _bookRepository;
        
        public BooksController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        [HttpPost]
        public ActionResult<ApiResponse<Book>> Post([FromBody] Book requestBody)
        {
            ValidateResult validateResult = requestBody.Validate();
            if (validateResult.Error)
            {
                return new ApiResponse<Book>(true, validateResult.Message, null);
            }

            var book = _bookRepository.GetByTitle(requestBody.Title);

            if (book != null)
            {
                return BadRequest(new ApiResponse<Book>(true, "There is already a record for this title.", null));
            }
            
            var createdBook = _bookRepository.Create(requestBody);

            return Created("", new ApiResponse<Book>(false, "Book successfully created", createdBook));
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<Book>> Delete(string id)
        {
            var deleteResult = _bookRepository.Delete(id);
            if (!deleteResult)
            {
                return BadRequest(new ApiResponse<Book>(true, "Something is wrong", null));
            }

            return Ok(new ApiResponse<Book>(false, "Deleted successfully", null));
        }
        
        [HttpPut("{id}")]
        public ActionResult<ApiResponse<Book>> Put(string id, [FromBody] Book requestBody)
        {
            var validateResult = requestBody.Validate();

            if (validateResult.Error)
            {
                return BadRequest(new ApiResponse<Book>(true, validateResult.Message, null));
            }

            requestBody.Id = new ObjectId(id);
            var updateResult = _bookRepository.Update(id, requestBody);
            if (!updateResult)
            {
                return BadRequest(new ApiResponse<Book>(true, "Something is wrong", null));
            }

            return Ok(new ApiResponse<Book>(false, "Updated successfully", requestBody));
        }

        [HttpGet]
        public ActionResult<ApiResponse<List<Book>>> Get()
        {
            var books = _bookRepository.GetList();
            return Ok(new ApiResponse<List<Book>>(false, "List successfully fetched", books));
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<Book>> Get(string id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound(new ApiResponse<Book>(true, "Book Id is not valid", null));
            }
            return Ok(new ApiResponse<Book>(false, "Book successfully fetched", book));
        }
    }
}