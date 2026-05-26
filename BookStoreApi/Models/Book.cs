using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required")]
        public string Title { get; set; } = string.Empty;

        [Range(1000, 2100, ErrorMessage = "Publication year must be valid")]
        public int PublicationYear { get; set; }

        [Required(ErrorMessage = "AuthorId is required")]
        public int AuthorId { get; set; }

        public Author? Author { get; set; }
    }
}