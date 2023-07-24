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
    public class GetAllOverdueItemsHandler : IRequestHandler<GetAllOverdueItems, ICollection<ToDoItem>>
    {

        private readonly IToDoRepository _toDoRepository;

        public GetAllOverdueItemsHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<ICollection<ToDoItem>> Handle(GetAllOverdueItems request, CancellationToken cancellationToken)
        {
            return await _toDoRepository.GetAllOverdueItems();
        }
    }
}
