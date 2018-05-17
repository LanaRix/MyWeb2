using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWeb2.Models;

namespace MyWeb2.Controllers
{
    [Produces("application/json")]
    [Route("api/Communications")]
    public class CommunicationsController : Controller
    {
        private readonly forInternetAppContext _context;

        public CommunicationsController(forInternetAppContext context)
        {
            _context = context;
        }

        // GET: api/Communications
        [HttpGet]
        public IEnumerable<Communication> GetCommunication()
        {
            return _context.Communication;
        }

        // GET: api/Communications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommunication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var communication = await _context.Communication.SingleOrDefaultAsync(m => m.CommunicateId == id);

            if (communication == null)
            {
                return NotFound();
            }

            return Ok(communication);
        }

        // PUT: api/Communications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommunication([FromRoute] int id, [FromBody] Communication communication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != communication.CommunicateId)
            {
                return BadRequest();
            }

            _context.Entry(communication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunicationExists(id))
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

        // POST: api/Communications
        [HttpPost]
        public async Task<IActionResult> PostCommunication([FromBody] Communication communication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Communication.Add(communication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommunication", new { id = communication.CommunicateId }, communication);
        }

        // DELETE: api/Communications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var communication = await _context.Communication.SingleOrDefaultAsync(m => m.CommunicateId == id);
            if (communication == null)
            {
                return NotFound();
            }

            _context.Communication.Remove(communication);
            await _context.SaveChangesAsync();

            return Ok(communication);
        }

        private bool CommunicationExists(int id)
        {
            return _context.Communication.Any(e => e.CommunicateId == id);
        }
    }
}