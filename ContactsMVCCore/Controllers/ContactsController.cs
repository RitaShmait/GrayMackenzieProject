using ContactsMVCCore.Context;
using ContactsMVCCore.Dtos;
using ContactsMVCCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsMVCCore.Controllers
{
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly ContactsDBContext _contactsDBContext;
        public ContactsController(ContactsDBContext contactsDBContext) 
        {
            _contactsDBContext = contactsDBContext;
        }

        [HttpPost("/AddItem")]
        public async Task<IActionResult> AddContact(AddContactInput input)
        {
            var contact = _contactsDBContext.Add(new Contact()
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                PhoneNumber = input.PhoneNumber,
            });
            await _contactsDBContext.SaveChangesAsync();
            return Ok("Added successfully!");
        }

        [HttpPut("/EditItem")]      
        public async Task<IActionResult> EditContact(EditContactInput input)
        {
            var contact = _contactsDBContext.Find<Contact>(input.Id);
            if (contact == null)
            {
                return Ok("Not Found");
            }
            contact.FirstName = input.FirstName;
            contact.LastName = input.LastName;
            contact.Email = input.Email;
            contact.PhoneNumber = input.PhoneNumber;
            _contactsDBContext.Update(contact);
            await _contactsDBContext.SaveChangesAsync();
            return Ok("Updated successfully!");
        }


        [HttpGet("/GetItems")]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _contactsDBContext.Contacts.ToListAsync();
            if (contacts == null)
            {
                return Ok("Not Found");
            }
            return Ok(contacts);
        }


        [HttpPost("/GetItemById")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var contact = _contactsDBContext.Find<Contact>(id);
            if (contact == null)
            {
                return Ok("Not Found");
            }
            return Ok(contact);
        }        
        
        [HttpDelete("/DeleteItem")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = _contactsDBContext.Find<Contact>(id);
            if(contact == null)
            {
               return Ok("Not Found");
            }
            _contactsDBContext.Remove<Contact>(contact);
            await _contactsDBContext.SaveChangesAsync();
            return Ok("Deleted!");
        }
    }
}
