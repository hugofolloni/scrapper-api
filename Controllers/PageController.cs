using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scrapper_api.Models;

namespace scrapper_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly PageContext _context;

        public PageController(PageContext context)
        {
            _context = context;
        }

        // GET: api/Page
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Page>>> GetPageItems()
        {
            return await _context.PageItems.ToListAsync();
        }

        // GET: api/Page/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Page>> GetPage(long id)
        {
            var page = await _context.PageItems.FindAsync(id);

            if (page == null)
            {
                return NotFound();
            }

            return page;
        }

        // PUT: api/Page/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPage(long id, Page page)
        {
            if (id != page.Id)
            {
                return BadRequest();
            }

            _context.Entry(page).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageExists(id))
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

        // POST: api/Page
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Page>> PostPage(Page page)
        {
            Scrapper sc = new Scrapper();
            int Count = sc.getCount(page.Name, page.Word);
            page.Count = Count;

            _context.PageItems.Add(page);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPage", new { id = page.Id }, page);
        }

        // DELETE: api/Page/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(long id)
        {
            var page = await _context.PageItems.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }

            _context.PageItems.Remove(page);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PageExists(long id)
        {
            return _context.PageItems.Any(e => e.Id == id);
        }
    }
}
