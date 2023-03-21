using News.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Api.Services
{
    public interface IMessagesRepository
    {
        Task<List<Message>> GetAll ();
        Task<Message> Get (string id);
        Task Create (Message message);
        Task Delete (string id);
        Task Update (string id, Message message);
    }
}
