using System;

namespace TodoDTO.Domain.Entites
{
    public class AuditableEntity
    {
        public DateTime CreatedTime { get; set; }
        public DateTime LastModifiedTime { set; get; }
    }
}