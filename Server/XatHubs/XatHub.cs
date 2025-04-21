using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Client.Tools;

namespace Server.XatHubs
{
    [Authorize(Roles = "User, Admin")]
    public class XatHub : Hub
    {
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            Console.WriteLine($"Client desconnectat: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(ex);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Nou client: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }
        public async Task EnviarMissatge(string missatge)
        {
            var userName = Context.User?.Identity?.Name ?? "Anònim";
            await Clients.All.SendAsync("RepMissatge", userName, missatge);
        }
        public async Task EnviarMissatgePrivat(string usuari, string missatge)
        {
            await Clients.User(usuari).SendAsync("RepMissatge", usuari, missatge);
        }
        public async Task EnviarMissatgeGrup(string grup, string missatge)
        {
            await Clients.Group(grup).SendAsync("RepMissatge", grup, missatge);
        }
        public async Task UnirGrup(string grup)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, grup);
            await Clients.Group(grup).SendAsync("RepMissatge", grup, "S'ha unit al grup");
        }
    }
}
