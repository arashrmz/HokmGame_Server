using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HokmGame_Server.Data;
public class Player
{
    public string ID { get; set; }
    public int PlayerNumber { get; set; }
    public User User { get; set; }
}