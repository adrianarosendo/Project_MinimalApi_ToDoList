
using Application.Abstractions;
using Application.ToDo.Queries;
using Application.ToDo.QueryHandlers;
using Domain.Models;
using Moq;


namespace ToDoListTest.EndpointTest
{
    public class QueryHandlerTest
    {

        [Fact]
        public void GetAllOverdueItemsOk()
        {

            // Arrange
            ToDoItem item = new ToDoItem()
            {
                Id = 1,
                Name = "Teste1",
                Deadline = DateTime.Now.AddDays(1),

            };

            ToDoItem item2 = new ToDoItem()
            {
                Id = 2,
                Name = "Teste2",
                Deadline = DateTime.Now.AddDays(-2),

            };



            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.GetAllOverdueItems())
                           .ReturnsAsync(new List<ToDoItem>() { item2 });

            var handler = new GetAllOverdueItemsHandler(mockSomeService.Object);

            var request = new GetAllOverdueItems
            {};

            // Act
            var response = handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Result.Contains(item2));


        }

        [Fact]
        public async Task GetAllOverdueItemsFailure()
        {
            // Arrange

            ToDoItem item = new ToDoItem()
            {
                Id = 1,
                Name = "Teste1",
                Deadline = DateTime.Now.AddDays(-1),
                IsComplete = true

            };

            ToDoItem item2 = new ToDoItem()
            {
                Id = 2,
                Name = "Teste2",
                Deadline = DateTime.Now.AddDays(-2),

            };

            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.GetAllOverdueItems())
                           .ThrowsAsync(new Exception("Service error"));

            var handler = new GetAllOverdueItemsHandler(mockSomeService.Object);

            var request = new GetAllOverdueItems
            { };

            // Act
            var response = handler.Handle(request, CancellationToken.None);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public void GetAllToDoItensOk()
        {

            // Arrange
            var name = "Teste";
            ToDoItem item = new ToDoItem()
            {
                Name = name,
                Deadline = DateTime.Now.AddDays(1),

            };


            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.GetAllToDo())
                .ReturnsAsync(new List<ToDoItem>() { item});
                           

            var handler = new GetAllToDoItensHandler(mockSomeService.Object);

            var request = new GetAllToDoItems
            {};

            // Act
            var response = handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Result.Contains(item));


        }

        [Fact]
        public async Task GetAllToDoItensFailure()
        {
            // Arrange
            ToDoItem item = new ToDoItem()
            {
                Name = null,
                Deadline = DateTime.Now.AddDays(1),

            };
            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.GetAllToDo())
                           .ThrowsAsync(new Exception("Service error"));

            var handler = new GetAllToDoItensHandler(mockSomeService.Object);

            var request = new GetAllToDoItems() { };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public void GetToDoItenByIdOk()
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
            mockSomeService.Setup(service => service.GetToDoById(1)).ReturnsAsync(item);

            var handler = new GetToDoItenByIdHandler(mockSomeService.Object);

            var request = new GetToDoItemById
            {
                ToDoId = item.Id
            };

            // Act
            var response = handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("Teste", response.Result.Name);



        }

        [Fact]
        public async Task GetToDoItenByIdFailure()
        {
            // Arrange
            ToDoItem item = new ToDoItem()
            {
                Id = 1,
                Name = null,
                Deadline = DateTime.Now.AddDays(1),

            };
            var mockSomeService = new Mock<IToDoRepository>();
            mockSomeService.Setup(service => service.GetToDoById(It.IsAny<int>()))
                           .ThrowsAsync(new Exception("Service error"));

            var handler = new GetToDoItenByIdHandler(mockSomeService.Object);

            var request = new GetToDoItemById
            {
                ToDoId = item.Id,
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));
        }


    }
}



