using Application.Abstractions;
using Application.ToDo.Commands;
using MediatR;


namespace Application.ToDo.CommandHandlers
{
    public class DeleteToDoHandler : IRequestHandler<DeleteToDo>
    {

        private readonly IToDoRepository _toDoRepository;

        public DeleteToDoHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
        public async Task Handle(DeleteToDo request, CancellationToken cancellationToken)
        {
            await _toDoRepository.DeleteToDo(request.ToDoId);

            return;
        }
    }
}
