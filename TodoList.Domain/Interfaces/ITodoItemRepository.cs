using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetCompletedTasksAsync();
        Task<IEnumerable<TodoItem>> GetIncompleteTasksAsync();
    }
}