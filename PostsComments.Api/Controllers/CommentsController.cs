using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostsComments.Api.Data;
using PostsComments.Api.Models;
using PostsComments.Api.Services;

namespace PostsComments.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentsController (ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments ()
        {
            return await _commentRepository.GetAllComments();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment (string id)
        {
            var comment = await _commentRepository.GetComment (id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }


        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment (Comment comment)
        {
            await _commentRepository.CreateComment(comment);
            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment (string id)
        {
            await _commentRepository.DeleteComment(id);
            return NoContent();
        }


    }
}
