using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Chat
{
    public class DetailsChat
    {
        public string sessionId { get; set; }
        public int id { get; set; }
        public int chatId { get; set; }
        public string message { get; set; }
        public bool isReading { get; set; }
    }
}
