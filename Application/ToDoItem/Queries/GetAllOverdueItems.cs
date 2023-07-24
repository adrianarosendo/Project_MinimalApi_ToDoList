

using Domain.Models;
using MediatR;

namespace Application.ToDo.Queries
{
    public class GetAllOverdueItems : IRequest<ICollection<ToDoItem>>
    {
    }
}
