using HokmGame_Server.Data;
using HokmGame_Server.Services;
using Microsoft.AspNetCore.SignalR;

namespace HokmGame_Server.Hubs;

public class ChatHub : Hub
{
    private IUserService _userService;
    private FriendService friendService;
    private GameManager gameManager;
    private PlayerManager playerManager;

    public ChatHub(IUserService userService, FriendService friendService, GameManager gameManager, PlayerManager playerManager)
    {
        _userService = userService;
        this.friendService = friendService;
        this.gameManager = gameManager;
        this.playerManager = playerManager;
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
            this.Context.Items.Add("user", result);
            await Clients.Caller.SendAsync("Login", "Success");
        }
        else
        {
            await Clients.Caller.SendAsync("Login", "Failed");
        }
    }

    //joins a room waiting for other player
    public Task JoinRoom()
    {
        return Task.Run(() =>
        {
            int playerNum;
            //Dynamically allocate number of rooms
            if (gameManager.CountInRoom == 2 || gameManager.CountInRoom == 0)
            {
                playerNum = 1;
                gameManager.CountInRoom = 1;
                gameManager.NumOfRooms++;
                Groups.AddToGroupAsync(Context.ConnectionId, gameManager.NumOfRooms.ToString());
                Clients.Caller.SendAsync("JoinRoom", gameManager.NumOfRooms.ToString());
            }
            else
            {
                playerNum = 2;
                gameManager.CountInRoom++;
                Groups.AddToGroupAsync(Context.ConnectionId, gameManager.NumOfRooms.ToString());
                //tell the room game is ready
                Clients.Group(gameManager.NumOfRooms.ToString()).SendAsync("GameReady", gameManager.NumOfRooms.ToString());
            }
            playerManager.PlayersRooms.Add(new Player
            {
                ID = Context.ConnectionId,
                PlayerNumber = playerNum,
                User = (User)Context.Items["user"]
            }, gameManager.NumOfRooms);
        });
    }

    //send initial game data (decks, players, etc.) to other player
    public Task<bool> StartGame(string roomName)
    {
        return Task.Run(() =>
        {
            try
            {
                //start the game
                Clients.OthersInGroup(roomName).SendAsync("StartGame");
                return true;
            }
            catch
            {
                return false;
            }
        });
    }

    //send initial game data (decks, players, etc.) to other player
    public Task<bool> GameData(string roomName, string data)
    {
        return Task.Run(() =>
        {
            try
            {
                //send game state so players build decks
                Clients.OthersInGroup(roomName).SendAsync("ReceiveGameData", data);
                return true;
            }
            catch
            {
                return false;
            }
        });
    }

    //player plays a card
    public Task<bool> PlayCard(string roomName, string card)
    {
        return Task.Run(() =>
        {
            try
            {
                //send card to other player
                Clients.OthersInGroup(roomName).SendAsync("ReceiveCard", card);
                return true;
            }
            catch
            {
                return false;
            }
        });
    }

    public async Task AddFriend(string name, int myId)
    {
        await _userService.AddFriend(name, myId);
    }

    public async Task<User> GetUser(int id)
    {
        return await _userService.GetById(id);
    }

    public async Task GetFriends(int myId)
    {
        var friends = _userService.GetFriends(myId);
        await Clients.Caller.SendAsync("ReceiveFriends", friends);
    }
}