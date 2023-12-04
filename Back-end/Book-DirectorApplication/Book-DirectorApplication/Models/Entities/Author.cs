using System.Text.Json.Serialization;

namespace Book_DirectorApplication.Models.Entities
{
    public class Author
    {
        public int? AuthorId { get; set; } 
        public string? Name { get; set; } 
        public int? BirthYear { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; }

    }
}
