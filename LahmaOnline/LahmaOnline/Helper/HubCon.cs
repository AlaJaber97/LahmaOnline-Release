using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Forms;

namespace LahmaOnline.Helper
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
            string url = "http://webapi.justdevelopmentjo.com/chatHub";
            //string url = "http://localhost:52352/chatHub";
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