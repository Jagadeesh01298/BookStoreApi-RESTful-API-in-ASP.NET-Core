using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        public string Name { get; set; } = string.Empty;

        public string? Country { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}