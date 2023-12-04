using Book_DirectorApplication.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_DirectorApplication.Models.DTOS
{
    public class BookDTO
    {
        public int? BookId { get; set; }

        public string? Title { get; set; }

        public int? PublicationYear { get; set; }

        public int? AuthorID { get; set; }

        [ForeignKey("AuthorID")]
        public Author? Author { get; set; }
    }
}
