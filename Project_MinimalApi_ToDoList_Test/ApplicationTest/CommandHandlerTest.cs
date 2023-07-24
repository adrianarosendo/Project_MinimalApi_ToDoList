
using Application.Abstractions;
using Application.ToDo.CommandHandlers;
using Application.ToDo.Commands;
using Domain.Models;
using Moq;


namespace ToDoListTest.CommandHandlerTest
{
    public class CommandHandlerTest
    {

        [Fact]
        public void CreateToDoItemOk()
        {

            // Arrange
            var name = "Teste";
            ToDoItem item = new ToDoItem()
            {
                Name = name,
                Deadline = DateTime.Now.AddDays(1),

            };

          
            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.CreateToDo(It.IsAny<ToDoItem>()))
                           .ReturnsAsync(new ToDoItem { Name = "Success" });

            var handler = new CreateToDoItemsHandler(mockSomeService.Object);

            var request = new CreateToDo
            {
                ToDoContent = item
            };

            // Act
            var response = handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("Success", response.Result.Name);


        }

        [Fact]
        public async Task CreateToDoItemFailure()
        {
            // Arrange
            ToDoItem item = new ToDoItem()
            {
                Name = null,
                Deadline = DateTime.Now.AddDays(1),

            };
            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.CreateToDo(It.IsAny<ToDoItem>()))
                           .ThrowsAsync(new Exception("Service error"));

            var handler = new CreateToDoItemsHandler(mockSomeService.Object);

            var request = new CreateToDo
            {
                ToDoContent = item
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public void UpdateToDoItemOk()
        {

            // Arrange
            var name = "Teste";
            ToDoItem item = new ToDoItem()
            {
                Name = name,
                Deadline = DateTime.Now.AddDays(1),
                LastUpdate = DateTime.Now,
                CreatedAt = DateTime.Now.AddDays(-1),

            };


            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.UpdateToDo(It.IsAny<ToDoItem>(),It.IsAny<int>()))
                           .ReturnsAsync(new ToDoItem { Name = "Success" });

            var handler = new UpdateToDoHandler(mockSomeService.Object);

            var request = new UpdateToDo
            {
                ToDoContent = item
            };

            // Act
            var response = handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("Success", response.Result.Name);


        }

        [Fact]
        public async Task UpdateToDoItemFailure()
        {
            // Arrange
            ToDoItem item = new ToDoItem()
            {
                Name = null,
                Deadline = DateTime.Now.AddDays(1),

            };
            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.UpdateToDo(It.IsAny<ToDoItem>(), It.IsAny<int>()))
                           .ThrowsAsync(new Exception("Service error"));

            var handler = new UpdateToDoHandler(mockSomeService.Object);

            var request = new UpdateToDo
            {
                ToDoContent = null
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public void DeleteToDoItemOk()
        {

            // Arrange
            var name = "Teste";
            ToDoItem item = new ToDoItem()
            {
                Id = 1,
                Name = name,
                Deadline = DateTime.Now.AddDays(1),

            };


            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.DeleteToDo(It.IsAny<int>()));

            var handler = new DeleteToDoHandler(mockSomeService.Object);

            var request = new DeleteToDo
            {
                ToDoId = item.Id
            };

            // Act
            var response = handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsCompletedSuccessfully);


        }

        [Fact]
        public async Task DeleteToDoItemFailure()
        {
            // Arrange
            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.DeleteToDo(It.IsAny<int>()))
                           .ThrowsAsync(new Exception("Service error"));

            var handler = new DeleteToDoHandler(mockSomeService.Object);

            var request = new DeleteToDo
            {
                ToDoId = 1,
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
        }


    }
}



