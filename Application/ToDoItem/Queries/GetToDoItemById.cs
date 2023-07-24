

using Domain.Models;
using MediatR;

namespace Application.ToDo.Queries
{
    public class GetToDoItemById : IRequest<ToDoItem>
    {
        public int ToDoId { get; set; }
    }
}
