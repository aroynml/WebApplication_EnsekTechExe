using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    /*
    [Route("api/[controller]")]
    [ApiController]
    public class EnsekMeterReadingsController : ControllerBase
    {
        private readonly AccountMeterDbContext _context;

        public EnsekMeterReadingsController(AccountMeterDbContext context)
        {
            _context = context;
        }

        // GET: api/EnsekMeterReadings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnsekMeterReading>>> GetEnsekMeterReading()
        {
            return await _context.EnsekMeterReading.ToListAsync();
        }

        // GET: api/EnsekMeterReadings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnsekMeterReading>> GetEnsekMeterReading(int id)
        {
            var ensekMeterReading = await _context.EnsekMeterReading.FindAsync(id);

            if (ensekMeterReading == null)
            {
                return NotFound();
            }

            return ensekMeterReading;
        }

        // PUT: api/EnsekMeterReadings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnsekMeterReading(int id, EnsekMeterReading ensekMeterReading)
        {
            if (id != ensekMeterReading.Id)
            {
                return BadRequest();
            }

            _context.Entry(ensekMeterReading).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnsekMeterReadingExists(id))
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

        // POST: api/EnsekMeterReadings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EnsekMeterReading>> PostEnsekMeterReading(EnsekMeterReading ensekMeterReading)
        {
            _context.EnsekMeterReading.Add(ensekMeterReading);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnsekMeterReading", new { id = ensekMeterReading.Id }, ensekMeterReading);
        }

        // DELETE: api/EnsekMeterReadings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EnsekMeterReading>> DeleteEnsekMeterReading(int id)
        {
            var ensekMeterReading = await _context.EnsekMeterReading.FindAsync(id);
            if (ensekMeterReading == null)
            {
                return NotFound();
            }

            _context.EnsekMeterReading.Remove(ensekMeterReading);
            await _context.SaveChangesAsync();

            return ensekMeterReading;
        }

        private bool EnsekMeterReadingExists(int id)
        {
            return _context.EnsekMeterReading.Any(e => e.Id == id);
        }
    }
    */
}
