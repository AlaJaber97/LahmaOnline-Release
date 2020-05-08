using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile.ChatModel
{
    public class SaveHeadeChatDto
    {
        public int id { get; set; }
        public string eMail { get; set; }
        public string mobile { get; set; }
        public string sessionId { get; set; }
    }
}
