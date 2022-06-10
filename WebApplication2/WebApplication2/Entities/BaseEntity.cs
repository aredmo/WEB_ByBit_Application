using System;

namespace WebApplication2.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime RecordingDate { get; set; }
    }
}