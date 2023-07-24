using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace ToDoList.Filter
{
    public class ToDoItemValidatorFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var item = context.GetArgument<ToDoItem>(1);
            if (string.IsNullOrEmpty(item.Name)) return await Task.FromResult(Results.BadRequest("Name not valid"));

            return await next(context);
        
        }
    }
}
