using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.DTOs
{
    public class BookDto
    {
        [Required(ErrorMessage = "Book title is required")]
        public string Title { get; set; } = string.Empty;

        [Range(1000, 2100, ErrorMessage = "Publication year must be valid")]
        public int PublicationYear { get; set; }

        [Required(ErrorMessage = "AuthorId is required")]
        public int AuthorId { get; set; }
    }
}
