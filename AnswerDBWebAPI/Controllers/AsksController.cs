using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnswerDBWebAPI.Models;
using AnswerDBWebAPI.Attributes;

namespace AnswerDBWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey] // EL APIKEY para seguridad 
    public class AsksController : ControllerBase
    {
        private readonly AnswersDBContext _context;

        public AsksController(AnswersDBContext context)
        {
            _context = context;
        }

        // GET: api/Asks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ask>>> GetAsks()
        {
          if (_context.Asks == null)
          {
              return NotFound();
          }
            return await _context.Asks.ToListAsync();
        }

        //Este Get permite obtener la info de la pregunta segun su estado
        [HttpGet("GetByFilterAskState")]
        public ActionResult<IEnumerable<Ask>> GetByFilterAskState(int pState)
        {
            var query = (from u in _context.Asks
                         where u.AskStatusId == pState
                         select new
                         {
                             u.AskId,
                             u.Date,
                             u.Ask1, //parametro se llama igual que la clase
                             u.UserId,
                             u.AskStatusId,
                             u.IsStrike,
                             u.ImageUrl,
                             u.AskDetail
                         }).ToList();

            //crear un objeto de tipo de DTO de retorno
            List<Ask> list = new List<Ask>();

            foreach (var item in query)
            {
                Ask NewItem = new()
                {
                    AskId = item.AskId,
                    Date = item.Date,
                    Ask1 = item.Ask1,
                    UserId = item.UserId,
                    AskStatusId = item.AskStatusId,
                    IsStrike = item.IsStrike,
                    ImageUrl = item.ImageUrl,
                    AskDetail = item.AskDetail
                };
                list.Add(NewItem);
            }
            if (list == null)
            {
                return NotFound();
            }
            return list;
        }

        // GET: api/Asks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ask>> GetAsk(long id)
        {
          if (_context.Asks == null)
          {
              return NotFound();
          }
            var ask = await _context.Asks.FindAsync(id);

            if (ask == null)
            {
                return NotFound();
            }

            return ask;
        }

        // PUT: api/Asks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsk(long id, Ask ask)
        {
            if (id != ask.AskId)
            {
                return BadRequest();
            }

            _context.Entry(ask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AskExists(id))
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

        // POST: api/Asks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ask>> PostAsk(Ask ask)
        {
          if (_context.Asks == null)
          {
              return Problem("Entity set 'AnswersDBContext.Asks'  is null.");
          }
            _context.Asks.Add(ask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsk", new { id = ask.AskId }, ask);
        }

        // DELETE: api/Asks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsk(long id)
        {
            if (_context.Asks == null)
            {
                return NotFound();
            }
            var ask = await _context.Asks.FindAsync(id);
            if (ask == null)
            {
                return NotFound();
            }

            _context.Asks.Remove(ask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AskExists(long id)
        {
            return (_context.Asks?.Any(e => e.AskId == id)).GetValueOrDefault();
        }
    }
}
