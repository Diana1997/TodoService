using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoDTO.Application.Common.Interfaces;
using TodoDTO.Application.TodoItems.Queries.Common;

namespace TodoDTO.Application.TodoItems.Queries.Get
{
    public class GetTodoItems :  IRequest<IList<TodoItemDTO>>
    {
        
    }

    public class GetTodoItemsHandler : IRequestHandler<GetTodoItems, IList<TodoItemDTO>>
    {
        private readonly ITodoContext _context;

        public GetTodoItemsHandler(ITodoContext context)
        {
            _context = context;
        }
        
        public  async Task<IList<TodoItemDTO>> Handle(GetTodoItems request, CancellationToken cancellationToken)
        {
            return await  _context.TodoItems
                .AsNoTracking()
                .Select(x => new TodoItemDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsComplete = x.IsComplete
                })
                .ToListAsync(cancellationToken);
        }
    }
}