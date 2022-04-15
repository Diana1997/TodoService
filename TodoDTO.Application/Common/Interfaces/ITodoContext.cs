using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoDTO.Domain.Entites;

namespace TodoDTO.Application.Common.Interfaces
{
    public interface ITodoContext
    {
        DbSet<TodoItem> TodoItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}