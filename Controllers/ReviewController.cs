using BookStore.Interfaces;
using BookStore.Mappers;
using BookStore.Models.DTOs.Review;
using BookStore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IBookRepo _bookRepo;

        public ReviewController(IReviewRepo reviewRepo, IBookRepo bookRepo)
        {
            _reviewRepo = reviewRepo;
            _bookRepo = bookRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewRepo.GetAllAsync();
            var reviewsDTO = reviews.Select(r => r.ToReviewDTO());
            return Ok(reviewsDTO);
        }

        [HttpPost]
        [Route("{bookId:int}")]
        public async Task<IActionResult> Create([FromBody] CreateReviewRequestDTO requestDTO, [FromRoute] int bookId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _bookRepo.ExistsByIdAsync(bookId))
            {
                return NotFound("Книга не найдена!");
            }
            var reviewModel = requestDTO.ToReviewFromCreateDTO(bookId);

            reviewModel.Created = DateTime.UtcNow;
            await _reviewRepo.CreateAsync(reviewModel);
            return Ok(reviewModel.ToReviewDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var review = await _reviewRepo.Delete(id);
            if (review is null)
            {
                return NotFound("Отзыв не найден!");
            }
            return Ok(review.ToReviewDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewRequestDTO requestDTO)
        {
            var review = await _reviewRepo.UpdateAsync(requestDTO.ToReviewFromUpdateDTO(), id);

            if(review is null)
            {
                return NotFound("Отзыв не найден");
            }

            return Ok(review.ToReviewDTO());
        }
    }
}
