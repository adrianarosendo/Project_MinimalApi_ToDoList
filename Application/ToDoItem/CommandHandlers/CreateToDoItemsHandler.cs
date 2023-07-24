using Application.Abstractions;
using Application.ToDo.Commands;
using Domain.Models;
using MediatR;

namespace Application.ToDo.CommandHandlers
{
    public class CreateToDoItemsHandler : IRequestHandler<CreateToDo, ToDoItem>
    {

        private readonly IToDoRepository _toDoRepository;

        public CreateToDoItemsHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
        public async Task<ToDoItem> Handle(CreateToDo request, CancellationToken cancellationToken)
        {
     
                var newToDo = new ToDoItem
                {
                    Name = request.ToDoContent.Name,
                    Deadline = request.ToDoContent.Deadline,
                };

                return await _toDoRepository.CreateToDo(newToDo);
    
        }
    }
}
