using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HokmGame_Server.Services;
public class GameManager
{
    private int numberOfRooms;  //number of rooms
    private int numberOfPlayersInLastRoom; // how many players are in last room created

    public GameManager()
    {
        numberOfRooms = 0;
        numberOfPlayersInLastRoom = 0;
    }

    public int NumOfRooms { get => numberOfRooms; set => numberOfRooms = value; }
    public int CountInRoom { get => numberOfPlayersInLastRoom; set => numberOfPlayersInLastRoom = value; }
}