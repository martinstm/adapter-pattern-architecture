using System.Collections.Generic;

namespace Adapter.Common.Models
{
    public class OutputModel
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public bool IsValid { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
