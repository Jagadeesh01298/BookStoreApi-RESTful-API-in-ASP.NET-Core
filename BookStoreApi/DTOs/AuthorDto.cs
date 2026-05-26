using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.DTOs
{
    public class AuthorDto
    {
        [Required(ErrorMessage = "Author name is required")]
        public string Name { get; set; } = string.Empty;

        public string? Country { get; set; }
    }
}