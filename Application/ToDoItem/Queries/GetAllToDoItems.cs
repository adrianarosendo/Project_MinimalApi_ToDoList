using Domain.Models;
using MediatR;


namespace Application.ToDo.Queries
{
    public class GetAllToDoItems : IRequest<ICollection<ToDoItem>>
    {
    }
}
