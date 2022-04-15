using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApiDTO.Api.Controllers;
using TodoDTO.Application.TodoItems.Commands.Delete;
using TodoDTO.Application.TodoItems.Commands.Upsert;
using TodoDTO.Application.TodoItems.Queries.Common;
using TodoDTO.Application.TodoItems.Queries.Get;
using TodoDTO.Application.TodoItems.Queries.GetById;


namespace TodoDTO.Api.Controllers
{
    public class TodoItemsController : ApiBaseController
    {
        [HttpGet]
        public async Task<IList<TodoItemDTO>> GetTodoItems()
        {
            return await Mediator.Send(new GetTodoItems());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(Guid id)
        {
            TodoItemDTO todoItem = await Mediator.Send(new GetTodoItemById(id));

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(Guid id, UpsertTodoItem command)
        {

            var todoItem = await Mediator.Send(new GetTodoItemById(id));
            if (todoItem == null)
            {
                return NotFound();
            }

            command.Id = id;
            Guid updatedId = await Mediator.Send(command);

            return Ok(updatedId);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(UpsertTodoItem command)
        {
            Guid id = await Mediator.Send(command);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = id },
                id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            await Mediator.Send(new DeleteTodoItem(id));

            return NoContent();
        }
    }
}