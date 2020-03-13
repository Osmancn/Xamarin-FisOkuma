using System.Collections.Generic;

namespace FisOkuma.Models
{
    public class SaveReceiptOperationResult
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public ReceiptStr Receipt { get; set; }
        public List<string> Lines { get; set; }
        public List<string> Logs { get; set; }
    }
}
