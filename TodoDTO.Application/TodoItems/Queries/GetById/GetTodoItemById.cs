using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoDTO.Application.Common.Interfaces;
using TodoDTO.Application.TodoItems.Queries.Common;

namespace TodoDTO.Application.TodoItems.Queries.GetById
{
    public class GetTodoItemById :  IRequest<TodoItemDTO>
    {
        public GetTodoItemById(Guid id)
        {
            Id = id;
        }
        public Guid Id {  get; }
    }

    public class GetTodoItemByIdHandler : IRequestHandler<GetTodoItemById, TodoItemDTO>
    {
        private readonly ITodoContext _context;

        public GetTodoItemByIdHandler(ITodoContext context)
        {
            _context = context;
        }
        
        public  async Task<TodoItemDTO> Handle(GetTodoItemById request, CancellationToken cancellationToken)
        {
            TodoItemDTO entity = await _context.TodoItems
                .Select(x => new TodoItemDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsComplete = x.IsComplete
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return entity;
        }
    }
}