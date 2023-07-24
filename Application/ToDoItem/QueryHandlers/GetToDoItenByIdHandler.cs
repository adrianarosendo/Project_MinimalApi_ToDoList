using Application.Abstractions;
using Application.ToDo.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ToDo.QueryHandlers
{
    public class GetToDoItenByIdHandler : IRequestHandler<GetToDoItemById, ToDoItem>
    {

        private readonly IToDoRepository _toDoRepository;

        public GetToDoItenByIdHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
        public async Task<ToDoItem> Handle(GetToDoItemById request, CancellationToken cancellationToken)
        {
            return await _toDoRepository.GetToDoById(request.ToDoId);
        }
    }
}
