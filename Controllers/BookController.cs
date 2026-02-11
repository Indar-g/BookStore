using BookStore.Interfaces;
using BookStore.Mappers;
using BookStore.Models.DTOs.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace BookStore.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepo _bookRepo;
        private readonly IAuthorRepo _authorRepo;
        private readonly IFileService _fileService;
        public BookController(IBookRepo bookRepo, IAuthorRepo authorRepo, IFileService fileService)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBookRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _authorRepo.ExistsById(dto.AuthorId))
            {
                return NotFound("Автор не найден");
            }

            if (await _bookRepo.ExistsByTitleAndAuthorId(dto.Title, dto.AuthorId))
            {
                return BadRequest("Книга уже существует");
            }

            string? imagePath = null;
            if (dto.BookImage != null)
            {
                imagePath = await _fileService.SaveFileAsync(dto.BookImage, "uploads");
            }

            var bookModel = dto.ToBookFromCreateDTO();
            bookModel.BookImage = imagePath;

            await _bookRepo.CreateAsync(bookModel);

            return Ok(bookModel.ToBookDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookRepo.GetAllAsync();

            var allBooksDTO = books.Select(book => book.ToBookDTO()).ToList();

            return Ok(allBooksDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateBookRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBook = await _bookRepo.GetByIdAsync(id);
            if (existingBook is null)
            {
                return NotFound("Книга не найдена");
            }

            string? newImagePath = existingBook.BookImage;

            if (dto.BookImage != null)
            {
                if (!string.IsNullOrEmpty(existingBook.BookImage))
                {
                    await _fileService.DeleteFileAsync(existingBook.BookImage);
                }
                newImagePath = await _fileService.SaveFileAsync(dto.BookImage, "uploads");
            }

            existingBook.Title = dto.Title;
            existingBook.Price = dto.Price;
            existingBook.Genre = dto.Genre;
            existingBook.AuthorId = dto.AuthorId;
            existingBook.BookImage = newImagePath;

            await _bookRepo.UpdateAsync(existingBook);

            return Ok(existingBook.ToBookDTO());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book is null)
            {
                return NotFound("Книга не найдена");
            }

            return Ok(book.ToBookDTO());
        }

        [HttpGet("GetByTitle")]
        public async Task<IActionResult> GetByTitle([FromQuery] string title)
        {
            var book = await _bookRepo.GetByTitleAsync(title);

            if (book is null)
            {
                return NotFound("Книга не найдена");
            }

            return Ok(book.ToBookDTO());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var bookExists = await _bookRepo.ExistsByIdAsync(id);

            if (!bookExists)
            {
                return NotFound("Книга не найдена");
            }
            await _bookRepo.DeleteAsync(id);

            return NoContent();
        }
    }
}
