using Domain.Models;


namespace Application.Abstractions
{
    public interface IToDoRepository
    {
        Task<ICollection<ToDoItem>> GetAllToDo();
        Task<ICollection<ToDoItem>> GetAllOverdueItems();
        Task<ToDoItem?> GetToDoById(int taskToDoId);
        Task<ToDoItem> CreateToDo(ToDoItem toCreate);
        Task<ToDoItem?> UpdateToDo(ToDoItem updatedContent, int toDoId);
        Task DeleteToDo(int ToDoId);


    }
}
