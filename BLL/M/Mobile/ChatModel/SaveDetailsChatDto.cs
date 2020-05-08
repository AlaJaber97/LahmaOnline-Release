using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.M.Mobile.ChatModel
{
    public class SaveDetailsChatDto
    {
        public int chatId { get; set; }
        public string sessionID { get; set; }
        public string message { get; set; }
    }
}
