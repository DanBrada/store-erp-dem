using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StoreERP.Hubs;

namespace StoreERP.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class HelloController: Controller
{
    private readonly IHubContext<HelloHub> _hubContext;

    public HelloController(IHubContext<HelloHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult<string>> Hello(string name)
    {
        await _hubContext.Clients.All.SendAsync("UpdateCount", name);
        return $"hello, {name}!";
    }
}