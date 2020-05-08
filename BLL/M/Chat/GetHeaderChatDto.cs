using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Chat
{
    public class GetHeaderChatDto
    {
        public int id { get; set; }
        public string eMail { get; set; }
        public string mobile { get; set; }
        public string sessionId { get; set; }
        public string supportEmail { get; set; }
    }

}
