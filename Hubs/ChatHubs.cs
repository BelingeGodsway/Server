using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class ChatHubs : Hub
    {
        public async Task SendMessage (string user, string message)
        {
            await Clients.All.SendAsync ("ReceiveMessage", user, message);
        }
    }
}
