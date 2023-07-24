using Application.Abstractions;
using Application.ToDo.Queries;
using Domain.Models;
using MediatR;


namespace Application.ToDo.QueryHandlers
{
    public class GetAllToDoItensHandler : IRequestHandler<GetAllToDoItems, ICollection<ToDoItem>>
    {

        private readonly IToDoRepository _toDoRepository;

        public GetAllToDoItensHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<ICollection<ToDoItem>> Handle(GetAllToDoItems request, CancellationToken cancellationToken)
        {
            return await _toDoRepository.GetAllToDo();
        }
    }
}
