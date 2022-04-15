using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoDTO.Application.Common.Exceptions;
using TodoDTO.Application.Common.Interfaces;
using TodoDTO.Domain.Entites;

namespace TodoDTO.Application.TodoItems.Commands.Upsert
{
    public class UpsertTodoItem : IRequest<Guid>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class UpsertTodoItemHandler : IRequestHandler<UpsertTodoItem, Guid>
    {
        private readonly ITodoContext _context;

        public UpsertTodoItemHandler(ITodoContext context)
        {
            _context = context;
        }
        
        public async Task<Guid> Handle(UpsertTodoItem request, CancellationToken cancellationToken)
        {
            TodoItem entity;
            if (request.Id == Guid.Empty)
            {
                entity = new TodoItem();
                _context.TodoItems.Add(entity);
            }
            else
            {
                entity = await _context.TodoItems
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(TodoItem), request.Id); 
                }
            }

            entity.Name = request.Name;
            entity.IsComplete = request.IsComplete;

            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}