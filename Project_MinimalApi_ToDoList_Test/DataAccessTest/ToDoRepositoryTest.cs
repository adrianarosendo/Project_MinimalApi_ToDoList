using Application.Abstractions;
using Domain.Models;
using Moq;

namespace ToDoListTest.RepositoryTest
{
    public class ToDoRepositoryTest
    {

        Mock<IToDoRepository> toDoRepositoryMock;

        [Fact]
        public void CreateToDoItemOk()
        {
            var name = "Teste";
            ToDoItem item = new ToDoItem()
            {
                Name = name,
                Deadline = DateTime.Now.AddDays(1),

            };

            toDoRepositoryMock = new Mock<IToDoRepository>();
            toDoRepositoryMock.Setup(x => x.CreateToDo(item)).ReturnsAsync(item);

            var result = toDoRepositoryMock.Object.CreateToDo(item);
            Assert.True(result.Result.Name == "Teste");
            Assert.True(result.Result.Deadline.Year == DateTime.Now.Year);

        }

        [Fact]
        public void GetToDoItemByIdOk()
        {
            var name = "Teste";
            ToDoItem item = new ToDoItem()
            {
                Id = 1,
                Name = name,
                Deadline = DateTime.Now.AddDays(1),

            };

            toDoRepositoryMock = new Mock<IToDoRepository>();
            toDoRepositoryMock.Setup(x => x.GetToDoById(1)).ReturnsAsync(item);

            var result = toDoRepositoryMock.Object.GetToDoById(item.Id);
            Assert.True(result.Result.Name == "Teste");
            Assert.True(result.Result.Deadline.Year == DateTime.Now.Year);

        }


        [Fact]
        public void GetToDoAllItemOk()
        {
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
                Deadline = DateTime.Now.AddDays(1),

            };

            toDoRepositoryMock = new Mock<IToDoRepository>();
            toDoRepositoryMock.Setup(x => x.GetAllToDo()).ReturnsAsync(new List<ToDoItem>() { item, item2 });

            var result = toDoRepositoryMock.Object.GetAllToDo();
            Assert.True(result.Result.Count == 2);

        }

        [Fact]
        public void GetAllOverdueItemsOk()
        {
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

            toDoRepositoryMock = new Mock<IToDoRepository>();
            toDoRepositoryMock.Setup(x => x.GetAllOverdueItems()).ReturnsAsync(new List<ToDoItem>() { item2 });

            var result = toDoRepositoryMock.Object.GetAllOverdueItems();
            Assert.True(result.Result.Count == 1);
            Assert.True(!result.Result.Contains(item));

        }

        [Fact]
        public void UpdateToDoItemOk()
        {
            ToDoItem item = new ToDoItem()
            {
                Id = 1,
                Name = "Teste1",
                Deadline = DateTime.Now.AddDays(1),

            };

            ToDoItem updateItem = new ToDoItem()
            {
                Id = 1,
                Name = "UpdateTest",
                Deadline = DateTime.Now.AddDays(1),

            };


            toDoRepositoryMock = new Mock<IToDoRepository>();
            toDoRepositoryMock.Setup(x => x.GetToDoById(item.Id)).ReturnsAsync(item);
            toDoRepositoryMock.Setup(x => x.UpdateToDo(updateItem, item.Id)).ReturnsAsync(updateItem);

            var result = toDoRepositoryMock.Object.UpdateToDo(updateItem, item.Id);
            Assert.True(result.Result.Name == "UpdateTest");

        }

        [Fact]
        public void DeleteToDoItemOk()
        {
            ToDoItem item = new ToDoItem()
            {
                Id = 1,
                Name = "Teste1",
                Deadline = DateTime.Now.AddDays(1),

            };



            toDoRepositoryMock = new Mock<IToDoRepository>();
            toDoRepositoryMock.Setup(x => x.GetToDoById(item.Id)).ReturnsAsync(item);
            toDoRepositoryMock.Setup(x => x.DeleteToDo(item.Id));

            var result = toDoRepositoryMock.Object.DeleteToDo(item.Id);
            Assert.True(result.IsCompletedSuccessfully);

        }
    }
}



