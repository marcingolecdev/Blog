using PostsComments.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostsComments.Api.Services
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComments ();
        Task<Comment> GetComment (string id);
        Task CreateComment (Comment comment);
        Task DeleteComment (string id);
    }
}
