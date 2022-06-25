using HokmGame_Server.Data;
using HokmGame_Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace HokmGame_Server.Hubs;

public class ChatHub : Hub
{
    private IUserService _userService;

    public ChatHub(IUserService userService)
    {
        _userService = userService;
    }

    public override Task OnConnectedAsync()
    {
        System.Console.WriteLine("Connected");
        return base.OnConnectedAsync();
    }

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    //register a new user
    public async Task Register(string user, string password)
    {
        var result = await _userService.Create(new User { Name = user, Password = password });
        if (result)
        {
            await Clients.Caller.SendAsync("Register", "Success");
        }
        else
        {
            await Clients.Caller.SendAsync("Register", "Failed");
        }
    }

    //login a user
    public async Task Login(string user, string password)
    {
        var result = _userService.GetByName(user);

        //verify password hash
        if (result != null && BCrypt.Net.BCrypt.Verify(password, result.Password))
        {
            await Clients.Caller.SendAsync("Login", "Success");
        }
        else
        {
            await Clients.Caller.SendAsync("Login", "Failed");
        }
    }
}
