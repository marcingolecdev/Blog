using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostsComments.Api.Controllers;
using PostsComments.Api.Models;
using PostsComments.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PostTest
{
    public class PostTest
    {

        private readonly Mock <IPostRepository> _postsRepository;
        private readonly PostsController _postsController;

        public PostTest ()
        {
            _postsRepository = new Mock<IPostRepository> ();
            _postsController = new PostsController (_postsRepository.Object);
        }



        [Fact]
        public async Task GetPost_ReturnNotFound_WhenItemDoesNotExists ()
        {
            // Arrange 
            _postsRepository.Setup(s => s.GetPost(It.IsAny<Guid>().ToString()))
                .ReturnsAsync((Post) null);

            // Act

            var result = await _postsController.GetPost (Guid.NewGuid ().ToString ());

            // Assert 
            result.Result.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public async Task GetPosts_ReturnMessage_WhenItemExists ()
        {
            // Arrange 
            var expectedItem = FakeData ();
            _postsRepository.Setup(s => s.GetPost(It.IsAny<string>()))
                .ReturnsAsync(expectedItem);

            // Act

            var result = await _postsController.GetPost (expectedItem.PostId);

            // Assert
            Assert.IsType<Post>(result.Value);
            var msg = (result as ActionResult <Post>).Value;
            Assert.Equal(expectedItem.PostId, msg.PostId);
            Assert.Equal(expectedItem.Title, msg.Title);
        }



        [Fact]
        public async Task GetPosts_ReturnMessages_WhenItemsExists ()
        {
            // Arrange 
            var expectedItems = new List <Post> ()
            {
                FakeData (), FakeData ()
            };
            _postsRepository.Setup(s => s.GetAllPosts()).ReturnsAsync(expectedItems);

            // Act

            var result = await _postsController.GetPosts ();

            // Assert
            Assert.IsType<List<Post>>(result.Value);
        }


        [Fact]
        public async Task CreatePost_ReturnSuccess_WhenItemCreated ()
        {
            // Arrange 
            var expectedItem = FakeData ();
            _postsRepository.Setup(s => s.CreatePost(It.IsAny<Post>()))
                .Returns(Task.FromResult(expectedItem));

            // Act

            var result = await _postsController.PostPost (expectedItem);

            // Assert
            var createdItem = (result.Result as CreatedAtActionResult).Value as Post;
            expectedItem.Should().BeEquivalentTo(createdItem, ops => ops.ComparingByMembers<Post>());
        }



        [Fact]
        public async Task UpdatePost_ReturnNoContent_WhenItemExists ()
        {
            // Arrange 
            var expectedItem = FakeData ();
            _postsRepository.Setup(s => s.GetPost(It.IsAny<Guid>().ToString()))
                .ReturnsAsync(expectedItem);

            var itemId = expectedItem.PostId;
            var itemToUpdate = new Post () { PostId = itemId, Title = "title", Description = "description" };

            // Act

            var result = await _postsController.PutPost (itemId, itemToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }


        private Post FakeData ()
        {
            return new Post()
            {
                PostId = Guid.NewGuid().ToString(),
                Title = "title",
                Description = "description",
                DataDodania = DateTime.Now.ToString()
            };
        }

    }
}
