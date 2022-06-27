using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HokmGame_Server.Data;

namespace HokmGame_Server.Services;
public class PlayerManager
{
    //shows which room the player are in
    private Dictionary<Player, int> playersRooms;

    public PlayerManager()
    {
        playersRooms = new Dictionary<Player, int>();
    }
    public Dictionary<Player, int> PlayersRooms { get { return playersRooms; } set { playersRooms = value; } }
}