using ToDoList.Extension;


var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();
var app = builder.Build();



app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (Exception e)
    {
        ctx.Response.StatusCode = 500;
        await ctx.Response.WriteAsync("An error ocurred");
    }
});

app.UseHttpsRedirection();

app.RegisterEndpointDefinition();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo list Api");
});
app.Run();
