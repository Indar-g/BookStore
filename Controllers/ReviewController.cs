using BookStore.Data.Extensions;
using BookStore.Interfaces;
using BookStore.Mappers;
using BookStore.Models.DTOs.Review;
using BookStore.Models.Entities;
using BookStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly IBookRepo _bookRepo;
        private readonly UserManager<AppUser> _userManager;

        public ReviewController(IReviewRepo reviewRepo, IBookRepo bookRepo, UserManager<AppUser> userManager)
        {
            _reviewRepo = reviewRepo;
            _bookRepo = bookRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewRepo.GetAllAsync();
            var reviewsDTO = reviews.Select(r => r.ToReviewDTO());
            return Ok(reviewsDTO);
        }

        [HttpPost]
        [Authorize]
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

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var reviewModel = requestDTO.ToReviewFromCreateDTO(bookId);
            reviewModel.AppUserId = appUser.Id;

            reviewModel.Created = DateTime.UtcNow;
            await _reviewRepo.CreateAsync(reviewModel);
            return Ok(reviewModel.ToReviewDTO());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var reviewModel = await _reviewRepo.GetByIdAsync(id);

            if (reviewModel is null)
            {
                return NotFound("Отзыв не найден");
            } 

            return Ok(reviewModel.ToReviewDTO());
        }

        [HttpDelete]
        [Authorize (Roles = "Admin")]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var review = await _reviewRepo.Delete(id);
            if (review is null)
            {
                return NotFound("Отзыв не найден!");
            }
            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        [Route("my/{id:int}")]
        public async Task<IActionResult> DeleteUserReview([FromRoute] int id)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var userId = user.Id;


            var deleted = await _reviewRepo.DeleteReviewOfUser(id, userId);

            if(!deleted)
            {
                return NotFound("Отзыв не найден или вы не имеете права его удалять");
            }

            return NoContent();
        }

        [HttpPut]
        [Authorize]
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
