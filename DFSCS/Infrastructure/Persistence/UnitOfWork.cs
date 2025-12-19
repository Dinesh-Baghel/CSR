using  Application.Common.Interfaces;
using  Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDapper _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private bool _disposed;
        public UnitOfWork(IDapper context) => _context = context;

        public IRepository<T> Repository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
                _repositories.Add(typeof(T), new Repository<T>(_context));
            return (IRepository<T>)_repositories[typeof(T)];
           
        }

        public Task CommitAsync() => Task.CompletedTask; // Add transaction logic if needed

        public void Dispose()
        {
            if (!_disposed)
            {
                _repositories.Clear();
                _disposed = true;
            }
        }
    }
}
