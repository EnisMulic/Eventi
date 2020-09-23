using System;
using System.Collections.Generic;
using System.Text;

namespace Eventi.Domain
{
    public class ChatMessage
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }
    }
}
