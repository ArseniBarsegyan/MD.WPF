using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiary.WPF.Models
{
    /// <summary>
    /// Implementation of <see cref="IRepository{TEntity}" />.
    /// </summary>
    public class NoteRepository : IDisposable, IRepository<Note>
    {
        private readonly ApplicationContext _dbContext;
        private bool _disposed;

        public NoteRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbSet<Note> DbSet => _dbContext.Set<Note>();

        public IQueryable<Note> GetAll()
        {
            return DbSet.Include(x => x.Photos);
        }

        public async Task<Note> GetByIdAsync(int? id)
        {
            var note = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            return note;
        }

        public void Create(Note item)
        {
            DbSet.Add(item);
        }

        public async Task<Note> DeleteAsync(int? id)
        {
            var item = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                DbSet.Remove(item);
            }
            return item;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Note item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
