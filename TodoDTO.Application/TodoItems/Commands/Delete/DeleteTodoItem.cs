using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApiDTO.Application.Common.Models;
using TodoDTO.Application.Common.Exceptions;
using TodoDTO.Application.Common.Interfaces;
using TodoDTO.Domain.Entites;

namespace TodoDTO.Application.TodoItems.Commands.Delete
{
    public class DeleteTodoItem : IRequest<Result>
    {
        public DeleteTodoItem(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    public class DeleteTodoItemHandler : IRequestHandler<DeleteTodoItem, Result>
    {
        private readonly ITodoContext _context;

        public DeleteTodoItemHandler(ITodoContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteTodoItem request, CancellationToken cancellationToken)
        {
            TodoItem entity = await _context.TodoItems
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            _context.TodoItems.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}