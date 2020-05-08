using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BLL.Chat
{
    public class HubCon
    {
        private static HubConnection _connection;

        public static HubConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = CreateConnectionHub();
                }

                return _connection;
            }
        }

        private static HubConnection CreateConnectionHub()
        {
            string url = "http://afashho-001-site2.ctempurl.com/chatHub";

            var Connection = new HubConnectionBuilder()
                 .WithUrl(url)
                 .WithAutomaticReconnect(new TimeSpan[]
                                         { TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(5) })
                 .Build();
            Connection.StartAsync();
            Connection.ServerTimeout = TimeSpan.FromMinutes(2);
            return Connection;
        }
    }

}
