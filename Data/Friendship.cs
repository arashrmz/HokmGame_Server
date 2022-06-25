
namespace HokmGame_Server.Data;
public class Friendship
{
    public int SenderId { get; set; }

    public virtual User Sender { get; set; }

    public int FriendId { get; set; }

    public virtual User Friend { get; set; }
}