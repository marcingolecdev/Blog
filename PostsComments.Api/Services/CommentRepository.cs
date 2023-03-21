using Microsoft.EntityFrameworkCore;
using PostsComments.Api.Data;
using PostsComments.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostsComments.Api.Services
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllComments ()
        {
            var comments = await _context.Comments
                .ToListAsync ();
            return comments;
        }

        public async Task<Comment> GetComment (string id)
        {
            var item = await _context.Comments
                .FirstOrDefaultAsync (f=> f.CommentId == id);
            return item;
        }

        public async Task CreateComment (Comment comment)
        { 
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }



        public async Task DeleteComment (string id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync (f=> f.CommentId == id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

    }
}