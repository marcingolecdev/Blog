using Microsoft.EntityFrameworkCore;
using News.Api.Data;
using News.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Api.Services
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly ApplicationDbContext _context;

        public MessagesRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetAll ()
        {
            var items = await _context.Messages.ToListAsync ();
            return items;
        }

        public async Task<Message> Get (string id)
        {
            var item = await _context.Messages.FirstOrDefaultAsync (f=> f.Id == id);
            return item;
        }

        public async Task Create (Message item)
        {
            item.Id = Guid.NewGuid().ToString();
            _context.Messages.Add(item);
            await _context.SaveChangesAsync();
        }


        public async Task Delete (string id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync (f=>f.Id == id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task Update (string id, Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
