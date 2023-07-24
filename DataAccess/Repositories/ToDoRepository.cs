using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly SocialDBContext _socialDBContext;

        public ToDoRepository(SocialDBContext socialDBContext) 
        { 
           _socialDBContext = socialDBContext;
        }

        public async Task<ToDoItem> CreateToDo(ToDoItem toCreate)
        {

            var isDuplicateItem = await GetDuplicate(toCreate.Name);

            if (isDuplicateItem != null)
            {
                return null;
            }

            toCreate.CreatedAt = DateTime.Now;
            toCreate.LastUpdate = DateTime.Now;
            _socialDBContext.ToDoItem.Add(toCreate);
            await _socialDBContext.SaveChangesAsync();
            return toCreate;
        }

        public async Task DeleteToDo(int toDoId)
        {
            var toDo = await _socialDBContext.ToDoItem.FirstOrDefaultAsync(p => p.Id == toDoId);
            if (toDo == null) {
                return;
            };

            _socialDBContext.ToDoItem.Remove(toDo);
            await _socialDBContext.SaveChangesAsync();

        }

        public async Task<ICollection<ToDoItem>> GetAllToDo()
        {
            return await _socialDBContext.ToDoItem.ToListAsync();
        }

        public async Task<ToDoItem?> GetToDoById(int toDoId)
        {

                return await _socialDBContext.ToDoItem.FirstOrDefaultAsync(p => p.Id == toDoId);

        }

        public async Task<ICollection<ToDoItem>> GetAllOverdueItems()
        {
            return await _socialDBContext.ToDoItem.Where(p=> p.Deadline < DateTime.Now && p.IsComplete == false).OrderBy(p => p.Deadline).ToListAsync();
        }

        public async Task<ToDoItem?> UpdateToDo(ToDoItem updatedContent, int toDoId)
        {
            var toDo = await GetToDoById(toDoId);
            if (toDo == null) { 
                return null; 
            }

            var verifyDuplicate = _socialDBContext.ToDoItem.FirstOrDefault(t => t.Name == updatedContent.Name && t.Id != toDo.Id);
            if (verifyDuplicate!= null)
            {
                return null;
            }


            toDo.LastUpdate = DateTime.Now;
            if(!toDo.Name.IsNullOrEmpty()) { 
                toDo.Name = updatedContent.Name; 
            }

            if(toDo.Deadline <= DateTime.MinValue) { 
                toDo.Deadline = updatedContent.Deadline; 
            }

            toDo.IsComplete = updatedContent.IsComplete;

            _socialDBContext.Update(toDo);
            _socialDBContext.SaveChanges(); 
            return toDo;
        }

        public async Task<ToDoItem?> GetDuplicate(string toDoName)
        {
            return await _socialDBContext.ToDoItem.FirstOrDefaultAsync(p => p.Name.Equals(toDoName));

        }
    }
}
