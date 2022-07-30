namespace HokmGame_Server.Data;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public virtual ICollection<Friend> FriendsOf { get; set; }
    public virtual ICollection<Friend> FriendsTo { get; set; }
}
