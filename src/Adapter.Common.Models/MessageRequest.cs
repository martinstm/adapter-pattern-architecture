using System;

namespace Adapter.Common.Models
{
    public class MessageRequest
    {
        public Guid Id { get; set; }
        public string Label { get; set; }
        public string Content { get; set; }
    }
}
