namespace TodoList.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItems { get; }
        Task<int> SaveChangesAsync();
    }
}