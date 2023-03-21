using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using News.Api.Controllers;
using News.Api.Models;
using News.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NewsTest
{
    public class NewsTest
    {
        
        private readonly Mock <IMessagesRepository> _messageRepository;
        private readonly MessagesController _messageController;

        public NewsTest ()
        {
            _messageRepository = new Mock<IMessagesRepository>();
            _messageController = new MessagesController (_messageRepository.Object);
        }
           

        [Fact]
        public async Task GetMessage_ReturnNotFound_WhenItemDoesNotExists ()
        {
            // Arrange 
            _messageRepository.Setup (s=> s.Get (It.IsAny <Guid> ().ToString ()))
                .ReturnsAsync ((Message) null);

            // Act

            var result = await _messageController.GetMessage (Guid.NewGuid ().ToString ());

            // Assert 
            result.Result.Should ().BeOfType <NotFoundResult> ();
        } 

        [Fact]
        public async Task GetMessage_ReturnMessage_WhenItemExists ()
        {
            // Arrange 
            var expectedItem = FakeData ();
            _messageRepository.Setup(s => s.Get(It.IsAny <string>()))
                .ReturnsAsync(expectedItem);

            // Act

            var result = await _messageController.GetMessage (expectedItem.Id);

            // Assert
            Assert.IsType<Message>(result.Value);
            var msg = (result as ActionResult <Message>).Value;
            Assert.Equal(expectedItem.Id, msg.Id);
            Assert.Equal(expectedItem.Title, msg.Title);    
        }



        [Fact]
        public async Task GetAllMessages_ReturnMessages_WhenItemsExists ()
        {
            // Arrange 
            var expectedItems = new List <Message> ()
            {
                FakeData (), FakeData ()
            };
            _messageRepository.Setup(s => s.GetAll ()).ReturnsAsync (expectedItems);

            // Act

            var result = await _messageController.GetMessages ();

            // Assert
            Assert.IsType<List<Message>>(result.Value); 
        } 
        

        [Fact]
        public async Task CreateMessage_ReturnSuccess_WhenItemCreated ()
        {
            // Arrange 
            var expectedItem = FakeData ();
            _messageRepository.Setup(s => s.Create(It.IsAny <Message> ()))
                .Returns(Task.FromResult(expectedItem));

            // Act

            var result = await _messageController.PostMessage (expectedItem);

            // Assert
            var createdItem = (result.Result as CreatedAtActionResult).Value as Message;
            expectedItem.Should().BeEquivalentTo(createdItem, ops => ops.ComparingByMembers<Message>());
        }



        [Fact]
        public async Task UpdateMessage_ReturnNoContent_WhenItemExists ()
        {
            // Arrange 
            var expectedItem = FakeData ();
            _messageRepository.Setup(s => s.Get (It.IsAny <Guid> ().ToString ()))
                .ReturnsAsync (expectedItem);
            
            var itemId = expectedItem.Id;
            var itemToUpdate = new Message () { Id = itemId, Title = "title", Description = "description" };

            // Act

            var result = await _messageController.PutMessage (itemId, itemToUpdate);

            // Assert
            result.Should ().BeOfType <NoContentResult> ();
        }




        private Message FakeData ()
        {
            return new Message ()
            {
                Id = Guid.NewGuid ().ToString(),
                Title = "title",
                Description = "description",
                DataDodania = "1/5/2023"
            };
        } 

    }
}
