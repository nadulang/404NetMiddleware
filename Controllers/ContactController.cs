using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactWebAPI.Models;

namespace ContactWebAPI.Controllers
{
    [Route("api/Contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ContactController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserContact>>> GetUserContacts()
        {
            return await _context.UserContacts.ToListAsync();
        }

        // GET: api/Contact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserContact>> GetUserContact(int id)
        {
            var userContact = await _context.UserContacts.FindAsync(id);

            if (userContact == null)
            {
                return NotFound();
            }

            return userContact;
        }

        // PUT: api/Contact/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserContact(int id, UserContact userContact)
        {
            if (id != userContact.Id)
            {
                return BadRequest();
            }

            _context.Entry(userContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserContactExists(id))
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

        // POST: api/Contact
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserContact>> PostUserContact(UserContact userContact)
        {
            _context.UserContacts.Add(userContact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserContact", new { id = userContact.Id }, userContact);
        }

        // DELETE: api/Contact/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserContact>> DeleteUserContact(int id)
        {
            var userContact = await _context.UserContacts.FindAsync(id);
            if (userContact == null)
            {
                return NotFound();
            }

            _context.UserContacts.Remove(userContact);
            await _context.SaveChangesAsync();

            return userContact;
        }

        private bool UserContactExists(int id)
        {
            return _context.UserContacts.Any(e => e.Id == id);
        }
    }
}
