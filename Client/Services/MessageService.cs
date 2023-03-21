using Blog.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blog.Client.Services
{
    public class MessageService
    {
        private readonly HttpClient _httpClient; 

        public MessageService (HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }


        public async Task<List<Message>> GetAllMessages ()
        {
            return await _httpClient.GetFromJsonAsync<List<Message>>("api/messages");
        }

        public async Task<Message> GetMessageById (string id)
        {
            var messages = await GetAllMessages ();
            var message = messages.FirstOrDefault (f=> f.Id == id);
            return message;
        }

        public async Task CreateMessage (Message message)
        {
            message.Id = Guid.NewGuid().ToString();
            message.DataDodania = DateTime.Now.ToString();
            await _httpClient.PostAsJsonAsync<Message>("api/messages", message);
        }


        public async Task EditMessage (string id, Message message)
        {
            await _httpClient.PutAsJsonAsync<Message>($"api/messages/{id}", message);
        }

        public async Task DeleteMessage (string id)
        {
            var result = await _httpClient.DeleteAsync ($"api/messages/{id}");
            result.EnsureSuccessStatusCode();
        }
    }
}
