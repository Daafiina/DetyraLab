using AutoMapper;
using Book_DirectorApplication;
using Book_DirectorApplication.Models.DTOS;
using Book_DirectorApplication.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Book_AuthorApplication.Controllers
{
    [ApiController]
    [Route("api/Authors")]
    public class AuthorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public AuthorController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context;
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int? id, [FromBody] Author updatedAuthor)
        {
            if (id != updatedAuthor.AuthorId)
            {
                return BadRequest();
            }

            var existingAuthor = _context.authors.Find(id);
            if (existingAuthor == null)
            {
                return NotFound("Author doesn't exist");
            }

            existingAuthor.Name = updatedAuthor.Name;
            existingAuthor.BirthYear = updatedAuthor.BirthYear;


            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.authors.Any(e => e.AuthorId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            var authors = await _context.authors.ToListAsync();
            var authorsDTO = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
            return Ok(authorsDTO);

        }

        [HttpPost]

        public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            _context.authors.Add(author);

            await _context.SaveChangesAsync();

            var newAuthor = _mapper.Map<AuthorDTO>(author);
            return CreatedAtAction(nameof(GetAuthors), new { id = author.AuthorId }, newAuthor);

        }
    }

}
