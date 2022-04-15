using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoDTO.Application.Common.Interfaces;
using TodoDTO.Domain.Entites;

namespace TodoDTO.Infrastructure.Persistence
{
    public class TodoContext : DbContext, ITodoContext
    {
        private readonly IDateTime _dateTime;

        public TodoContext(
            IDateTime dateTime,
            DbContextOptions<TodoContext> options)
            : base(options)
        {
            _dateTime = dateTime;
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedTime = _dateTime.Now;
                        entry.Entity.LastModifiedTime = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedTime = _dateTime.Now;
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);
            
            return result;
        }
    }
}