using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Data;

namespace TodoList.Infrastructure.Repositories
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoListDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TodoItem>> GetCompletedTasksAsync()
        {
            return await _dbSet.Where(x => x.IsCompleted).ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteTasksAsync()
        {
            return await _dbSet.Where(x => !x.IsCompleted).ToListAsync();
        }
    }
}