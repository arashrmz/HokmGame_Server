namespace HokmGame_Server.Data;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public virtual ICollection<Friendship> Friends { get; set; }
}
