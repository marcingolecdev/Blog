using Blog.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blog.Client.Services
{
    public class CommentService
    {
        private readonly HttpClient _httpClient;

        public CommentService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Comment>> GetAllComments ()
        {
            return await _httpClient.GetFromJsonAsync<List<Comment>>("api/comments");
        }

        public async Task<Comment> GetCommentById (string id)
        {
            var comments = await GetAllComments ();
            var comment = comments.FirstOrDefault (f=> f.PostId == id);
            return comment;
        }

        public async Task CreateComment (Comment comment)
        {
            comment.CommentId = Guid.NewGuid().ToString();
            comment.DataDodania = DateTime.Now.ToString();
            await _httpClient.PostAsJsonAsync<Comment>("api/comments", comment);
        }

        public async Task DeleteComment (string id)
        {
            var result = await _httpClient.DeleteAsync ($"api/comments/{id}");
            result.EnsureSuccessStatusCode();
        }


        public async Task<List<Comment>> GetCommentsFromPost (string postId)
        {
            var comments = (await GetAllComments ())
                .Where (w=> w.PostId == postId)
                .ToList ();
            return comments;
        }

        public async Task DeleteNecessary (List<Comment> commentsToDelete)
        {
            foreach (var comment in commentsToDelete)
            {
                await DeleteComment(comment.CommentId);
            }
        }


    }
}
