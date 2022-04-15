using System;
using TodoDTO.Application.Common.Interfaces;

namespace TodoDTO.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}