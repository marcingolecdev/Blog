using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using News.Api.Data;
using News.Api.Models;
using News.Api.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesRepository _messageRepository;

        public MessagesController (IMessagesRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages ()
        {
            return await _messageRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage (string id)
        {
            var message = await _messageRepository.Get (id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage (string id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }
            await _messageRepository.Update(id, message);

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage (Message message)
        {
            await _messageRepository.Create(message);
            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage (string id)
        {
            await _messageRepository.Delete(id);

            return NoContent();
        }
    }
}
