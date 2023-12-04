using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Book_DirectorApplication.Models.Entities
{
    public class Book
    {
        public int? BookId { get; set; }    

        public string? Title { get; set; }   

        public int? PublicationYear { get; set; }

        public int? AuthorID { get; set; }

        [ForeignKey("AuthorID")]
        public Author? Author { get; set; }

    }
}
