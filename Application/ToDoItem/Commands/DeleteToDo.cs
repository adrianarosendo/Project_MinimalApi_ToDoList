
using MediatR;


namespace Application.ToDo.Commands
{
    public class DeleteToDo : IRequest
    {
        public int ToDoId { get; set; }
    }
}
