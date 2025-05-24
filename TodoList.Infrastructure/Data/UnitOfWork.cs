using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Repositories;

namespace TodoList.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoListDbContext _context;
        private ITodoItemRepository _todoItemRepository;

        public UnitOfWork(TodoListDbContext context)
        {
            _context = context;
        }

        public ITodoItemRepository TodoItems
        {
            get
            {
                _todoItemRepository ??= new TodoItemRepository(_context);
                return _todoItemRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}