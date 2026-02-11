using BookStore.Models.DTOs.Author;
using BookStore.Models.Entities;

namespace BookStore.Mappers
{
    public static class AuthorMappers
    {
        public static AuthorDTO ToAuthorDTO(this Author authorModel)
        {
            return new AuthorDTO
            {
                Id = authorModel.Id,
                Name = authorModel.Name,
                Books = authorModel.Books.Select(b => b.ToBookDTO()).ToList(),
                
            };
        }

        public static Author ToAuthorFromCreateDTO(this CreateAuthorRequestDTO authorDTO)
        {
            return new Author
            {
                Name = authorDTO.Name
            };
        }

        public static Author ToAuthorFromUpdateDTO(this UpdateAuthorRequestDTO authorDTO)
        {
            return new Author
            {
                Name = authorDTO.Name
            };
        }
    }
}
