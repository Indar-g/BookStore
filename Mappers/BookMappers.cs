using BookStore.Models.DTOs.Book;
using BookStore.Models.Entities;

namespace BookStore.Mappers
{
    public static class BookMappers
    {
        public static BookDTO ToBookDTO(this Book bookModel)
        {
            return new BookDTO
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Genre = bookModel.Genre,
                Price = bookModel.Price,
                AuthorId = bookModel.AuthorId,
                BookImage = bookModel.BookImage,
                Reviews = bookModel.Reviews.Select(r => r.ToReviewDTO()).ToList()
            };
        }
        public static Book ToBookFromCreateDTO(this CreateBookRequestDTO bookDTO)
        {
            return new Book
            {
                Title = bookDTO.Title,
                Genre = bookDTO.Genre,
                Price = bookDTO.Price,
                AuthorId= bookDTO.AuthorId,
                BookImage = null
            };
        }
        public static Book ToBookFromUpdateDTO(this UpdateBookRequestDTO bookDTO)
        {
            return new Book
            {
                Title = bookDTO.Title,
                Genre = bookDTO.Genre,
                Price = bookDTO.Price,
                AuthorId = bookDTO.AuthorId,
            };
        }
    }
}
