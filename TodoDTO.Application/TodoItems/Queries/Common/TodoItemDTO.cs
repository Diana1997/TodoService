using System;

namespace TodoDTO.Application.TodoItems.Queries.Common
{
    public class TodoItemDTO
    {

        public Guid Id {  get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}