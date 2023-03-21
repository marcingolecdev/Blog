using Microsoft.EntityFrameworkCore;
using PostsComments.Api.Data;
using PostsComments.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostsComments.Api.Services
{
    public class PostRepository
    {
        public class PostResository : IPostRepository
        {
            private readonly ApplicationDbContext _context;

            public PostResository (ApplicationDbContext context)
            {
                _context = context;
            }


            public async Task<List<Post>> GetAllPosts ()
            {
                var items = await _context.Posts
                .ToListAsync ();
                return items;
            }

            public async Task<Post> GetPost (string id)
            {
                var item = await _context.Posts
                .FirstOrDefaultAsync (f=> f.PostId == id);
                return item;
            }

            public async Task CreatePost (Post post)
            {
                post.PostId = Guid.NewGuid().ToString();
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
            } 


            public async Task UpdatePost (string id, Post post)
            {
                _context.Entry(post).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }


            public async Task DeletePost (string id)
            {
                var post = await _context.Posts.FirstOrDefaultAsync (f=> f.PostId == id);
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }


        }
    }
}
