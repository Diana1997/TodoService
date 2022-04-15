using System;

namespace TodoDTO.Domain.Entites
{
    public class TodoItem : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}