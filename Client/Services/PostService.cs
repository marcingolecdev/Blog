using Blog.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blog.Client.Services
{
    public class PostService
    {
        private readonly HttpClient _httpClient;

        public PostService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        /*
         Retrieving all items from controller "PostComments.cs" located in
          "PostsComments.Api". After a positive download, we get a list of posts that are displayed
          in the "Posts.razor" component. To be able to use this method, you need to do dependency injection
          in "Program.cs" and "_Imports.razor"
        */

        public async Task<List<Post>> GetAllPosts ()
        {
            return await _httpClient.GetFromJsonAsync<List<Post>>("api/posts");
        }

        /*
         Retrieving one element from the database based on Id.
          First, all posts are downloaded to the variable, and then they are filtered,
          thanks to the "FirstOrDefault()" method, we can find the post we are interested in. This method is one of the methods
          Linq, which is responsible for filtering elements.
        */
        public async Task<Post> GetPostById (string id)
        {
            return await _httpClient.GetFromJsonAsync<Post>($"api/posts/{id}");
        }


        /*
         Creating a new post. The method was used in the "CreatePost.razor" component.
          The data entered by the user is collected from the component's editing form and transferred
          to the post variable in the PostAsJsonAsync method, the method also contains the part of the api it refers to,
          the initial part of the api is defined as "Program.cs".
          We also see here that the Guid and the date are added.
        */
        public async Task CreatePost (Post post)
        {
            post.PostId = Guid.NewGuid().ToString();
            post.DataDodania = DateTime.Now.ToString();
            await _httpClient.PostAsJsonAsync<Post>("api/posts", post);
        }

        /*
        Deleting a post, the method connects to the api where the responsible method is defined
          for searching for an element based on Id. This method is only used by the "Administrator"
          and appears in the "DeletePost.razor" component
        */
        public async Task DeletePost (string id)
        {
            var result = await _httpClient.DeleteAsync ($"api/posts/{id}");
            result.EnsureSuccessStatusCode();
        }

        /*
         Editing a post works similarly to the one above, only with this addition
          the "post" object is passed, the data of which are taken from the "EditForm" editing form
          located in "EditPost.razor"
        */
        public async Task EditPost (string id, Post post)
        {
            await _httpClient.PutAsJsonAsync<Post>($"api/posts/{id}", post);
        }

    }
}

/*

This is a class that connects via the api interface to the "PostsComments.Api" service
and "HttpClient" is used for this, which contains built-in methods responsible for sending
and downloading data, i.e. it supports methods such as Get, Post, Put and Delete.
This version of "HttpClient" includes the "using System.Net.Http.Json" extension in the using sections
thanks to this, we can use more efficient methods such as "GetFromJsonAsync()",
or "PostAsJsonAsync()".
To be able to use this service, you need to register an instance of this class in the DependencyInjection container,
in the configuration file of this application "Program.cs"
 */