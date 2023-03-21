using PostsComments.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostsComments.Api.Services
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPosts ();
        Task<Post> GetPost (string id);
        Task CreatePost (Post post);
        Task DeletePost (string id);
        Task UpdatePost (string id, Post post);
    }
}
