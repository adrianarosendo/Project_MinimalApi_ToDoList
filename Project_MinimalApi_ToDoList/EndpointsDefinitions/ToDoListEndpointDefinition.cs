using Application.ToDo.Commands;
using Application.ToDo.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Abstraction;
using ToDoList.Filter;

namespace ToDoList.EndpointsDefinitions
{
    public class ToDoListEndpointDefinition : IEndpointDefinition
    {

        public void RegisterEndpoints(WebApplication app)
        {
            var toDoItem = app.MapGroup("/api/item");

            toDoItem.MapGet("/{id}", GetItemById)
            .WithName("GetItemById");

            toDoItem.MapPost("/", CreateItem)
                .AddEndpointFilter<ToDoItemValidatorFilter>();

            toDoItem.MapGet("/", GetAllToDoItems);

            toDoItem.MapGet("/overdue", GetAllOverdueItems);

            toDoItem.MapPut("/{id}", Put)
                .AddEndpointFilter<ToDoItemValidatorFilter>();

            toDoItem.MapDelete("/{id}", Delete);
        }

        private async Task<IResult> GetAllToDoItems(IMediator mediator)
        {
            var getCommand = new GetAllToDoItems { };
            var item = await mediator.Send(getCommand);
            return TypedResults.Ok(item);

        }

        private async Task<IResult> GetAllOverdueItems(IMediator mediator)
        {
            var getCommand = new GetAllOverdueItems { };
            var item = await mediator.Send(getCommand);
            return TypedResults.Ok(item);

        }

        private async Task<IResult> GetItemById(IMediator mediator, int id)
        {
            var getItem = new GetToDoItemById { ToDoId = id };
            var item = await mediator.Send(getItem);
            return TypedResults.Ok(item);

        }

        private async Task<IResult> CreateItem(IMediator mediator, ToDoItem toDoItem)
        {
            var createToDoItem = new CreateToDo { ToDoContent = toDoItem };
            var Item = await mediator.Send(createToDoItem);
            if(Item == null)
            {
                return Results.BadRequest("Duplicate Task");
            }
            return Results.CreatedAtRoute("GetItemById", new { Item.Id } , Item);

        }

        private async Task<IResult> Put(IMediator mediator, ToDoItem toDoItem, int id)
        {
            var updateItem = new UpdateToDo { ToDoId = id, ToDoContent = toDoItem };
            var updatedItem = await mediator.Send(updateItem);
            if (updatedItem == null)
            {
                return Results.BadRequest("Duplicate Task");
            }
            return TypedResults.Ok(updatedItem);

        }

        private async Task<IResult> Delete(IMediator mediator, int id)
        {
            var deleteItem= new DeleteToDo { ToDoId = id };
            await mediator.Send(deleteItem);
            return TypedResults.NoContent();

        }
    }
}
