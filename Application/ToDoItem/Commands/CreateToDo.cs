using Domain.Models;
using MediatR;

namespace Application.ToDo.Commands
{
    public class CreateToDo : IRequest<ToDoItem>
    {
        public ToDoItem? ToDoContent { get; set; }

    }
}
