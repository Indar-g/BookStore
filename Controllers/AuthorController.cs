using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Mappers;
using BookStore.Models.DTOs.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepo _authorRepo;
        public AuthorController(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var author = await _authorRepo.GetByIdAsync(id);
            if (author is null)
            {
                return NotFound("Автор не найден");
            }

            return Ok(author.ToAuthorDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorRepo.GetAllAsync();

            var authorDTO = authors.Select(a => a.ToAuthorDTO());

            return Ok(authorDTO);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var author = await _authorRepo.GetByNameAsync(name);

            if (author is null)
            {
                return NotFound("Автор не найден");
            }

            var authorDTO = author.ToAuthorDTO();

            return Ok(authorDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAuthorRequestDTO authorDTO)
        {
            var authorModel = authorDTO.ToAuthorFromCreateDTO();

            var exists = await _authorRepo.Exists(authorModel.Name);

            if (exists)
            {
                return BadRequest("Автор уже существует");
            }

            await _authorRepo.CreateAsync(authorModel);

            return CreatedAtAction(nameof(GetByName), new { name = authorModel.Name }, authorModel.ToAuthorDTO());
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateAuthorRequestDTO authorDTO)
        {
            var authorModel = await _authorRepo.UpdateAsync(id, authorDTO);

            if (authorModel is null)
            {
                return NotFound("Автор не найден");
            }

            return Ok(authorModel.ToAuthorDTO());
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var authorModel = await _authorRepo.Delete(id);

            if (authorModel is null)
            {
                return NotFound("Автор не найден");
            }
            return Ok(authorModel.ToAuthorDTO());
        }
    }
}
