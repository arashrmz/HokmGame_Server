using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HokmGame_Server.Data;

public class Friend
{
    public int ID { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int FriendId { get; set; }
    public virtual User FriendUser { get; set; }
}