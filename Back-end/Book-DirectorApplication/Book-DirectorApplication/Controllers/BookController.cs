using AutoMapper;
using Book_DirectorApplication.Models.DTOS;
using Book_DirectorApplication.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Book_DirectorApplication.Controllers
{
    [ApiController]
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public BookController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks() 
        {
            var books = await _context.books.Include(b => b.Author).ToListAsync();
            var booksDTO = _mapper.Map<IEnumerable<BookDTO>>(books);
            return Ok(booksDTO);

        }

        [HttpPost]

        public async Task<ActionResult<BookDTO>> CreateBooks(BookDTO bookDTO) 
        {
            var book = _mapper.Map<Book>(bookDTO);
            _context.books.Add(book);
            await _context.SaveChangesAsync();
            var newBook = _mapper.Map<BookDTO>(book);
            return CreatedAtAction(nameof(GetBooks), new { id = book.BookId }, newBook);

        }

        [HttpGet("FilterById")]
        public IActionResult GetBooksByYear(int? vitiPublikimit)
        {
            if (!vitiPublikimit.HasValue) 
            {
                return NotFound("Nuk keni shtypur asnje vit");
            }
            var filteredBooks = _context.books.Include(b => b.Author).Where(b => b.PublicationYear == vitiPublikimit).ToList();
            return Ok(filteredBooks);
        
        }

        [HttpGet("FilterByAuthor")]

        public IActionResult GetBooksByAuthor(string? author) 
        {
            if (author == null)
            {
                return NotFound("Nuk u gjet asnje autor me kete emer");
            }
            var filteredAuthors = _context.books.Include(b => b.Author).Where(b => b.Author.Name == author).ToList();
            return Ok(filteredAuthors);
        }
    




    }
}
