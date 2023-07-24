using Application.Abstractions;
using Application.ToDo.Commands;
using Domain.Models;
using MediatR;

namespace Application.ToDo.CommandHandlers
{
    public class UpdateToDoHandler : IRequestHandler<UpdateToDo, ToDoItem>
    {

        private readonly IToDoRepository _toDoRepository;

        public UpdateToDoHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
        public async Task<ToDoItem> Handle(UpdateToDo request, CancellationToken cancellationToken)
        {
            var updateToDo = await _toDoRepository.UpdateToDo(request.ToDoContent, request.ToDoId);
            return updateToDo;
        }
    }
}
