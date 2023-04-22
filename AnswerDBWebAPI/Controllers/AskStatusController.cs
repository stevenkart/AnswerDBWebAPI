using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnswerDBWebAPI.Models;

namespace AnswerDBWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AskStatusController : ControllerBase
    {
        private readonly AnswersDBContext _context;

        public AskStatusController(AnswersDBContext context)
        {
            _context = context;
        }

        // GET: api/AskStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AskStatus>>> GetAskStatuses()
        {
          if (_context.AskStatuses == null)
          {
              return NotFound();
          }
            return await _context.AskStatuses.ToListAsync();
        }

        // GET: api/AskStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AskStatus>> GetAskStatus(int id)
        {
          if (_context.AskStatuses == null)
          {
              return NotFound();
          }
            var askStatus = await _context.AskStatuses.FindAsync(id);

            if (askStatus == null)
            {
                return NotFound();
            }

            return askStatus;
        }

        // PUT: api/AskStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAskStatus(int id, AskStatus askStatus)
        {
            if (id != askStatus.AskStatusId)
            {
                return BadRequest();
            }

            _context.Entry(askStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AskStatusExists(id))
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

        // POST: api/AskStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AskStatus>> PostAskStatus(AskStatus askStatus)
        {
          if (_context.AskStatuses == null)
          {
              return Problem("Entity set 'AnswersDBContext.AskStatuses'  is null.");
          }
            _context.AskStatuses.Add(askStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAskStatus", new { id = askStatus.AskStatusId }, askStatus);
        }

        // DELETE: api/AskStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAskStatus(int id)
        {
            if (_context.AskStatuses == null)
            {
                return NotFound();
            }
            var askStatus = await _context.AskStatuses.FindAsync(id);
            if (askStatus == null)
            {
                return NotFound();
            }

            _context.AskStatuses.Remove(askStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AskStatusExists(int id)
        {
            return (_context.AskStatuses?.Any(e => e.AskStatusId == id)).GetValueOrDefault();
        }
    }
}
