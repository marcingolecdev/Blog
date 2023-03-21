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
    /*
     Kontroler udostępnia api do którego odnoszą się aplikacje klienckie.
     Kontroler ten odpowiada za kontakt z bazą danych poprz repozytorium
     */


    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController (IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }


        /*
            Występuje tu atrybut "[HttpGet]", oznacza to, dane z bazy będą pobierane w tym przypadku do listy.
            Kontroler ten korzysta z repozytorium, w którym zdefiniowane są metody odpowiadające za 
            transfer danych z i do bazy. Aby móc korzystać z tego repozytorium należy zdefiniować jego wystąpienie
            w konstruktorze oraz DI w pliku konfiguracyjnym.
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts ()
        {
            return await _postRepository.GetAllPosts();
        }


        /*
            Metoda pobiera Post na podstawie identyfikatora Id, jeżeli pobrany wynik jest null,
            zwraca "NotFound()", w przeciwnym razie zwracany jest post.
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost (string id)
        {
            var post = await _postRepository.GetPost (id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }



        /*
            Metoda odpowiada za edycję wpisu w bazie, na podstawie id odnajdywany jest post w bazie,
            kontent "post" zapisywany jest w bazie.
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost (string id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }
            await _postRepository.UpdatePost(id, post);

            return NoContent();
        }


        /*
            Metoda zapisuje post do bazy, zawiera parametr "post", do której musi zostać przekazany,
            po zapisaniu rekordu metoda da odnosi się do metody "GetPost", aby zwrócić jej wynik,
            a zwróconym wynikiem będzie post zapisany w bazie.
         */
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost (Post post)
        {
            await _postRepository.CreatePost(post);
            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }

        /*
            Usuwanie rekordu, w metodzie "DeletePost" zdefiniowany jest kod, któru usuwa rekord z bazy.
            W tej metodzie zwracany jest wynik "NoContent()", który mówi o tym, że rekordu nie ma w bazie.
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost (string id)
        {
            await _postRepository.DeletePost(id);
            return NoContent();
        }

    }
}
