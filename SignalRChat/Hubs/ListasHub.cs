using Microsoft.AspNetCore.SignalR;
namespace SignalRChat.Hubs
{
    public class ListasHub: Hub
    {
        public async Task SendChange(string lista, string message)
        {
            await Clients.All.SendAsync("ReceiveChange", lista, message);

        }

    }
}
