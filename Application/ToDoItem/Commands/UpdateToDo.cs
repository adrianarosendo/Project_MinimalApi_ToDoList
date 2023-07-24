using Domain.Models;
using MediatR;

namespace Application.ToDo.Commands
{
    public class UpdateToDo : IRequest<ToDoItem>
    {
        public int ToDoId { get; set; }
        public ToDoItem? ToDoContent { get; set; }

    }
}
