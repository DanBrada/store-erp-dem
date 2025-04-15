using Microsoft.AspNetCore.SignalR;

namespace StoreERP.Hubs;

public class HelloHub: Hub
{
    public async Task CountHello()
    {
        await Clients.All.SendAsync("UpdateCount","");
    }
}