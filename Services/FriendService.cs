using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HokmGame_Server.Data;

namespace HokmGame_Server.Services;

public class FriendService
{
    private AppDbContext _context;

    public FriendService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddFriend(User sender, User friend)
    {
        // var friendship = new Friendship
        // {
        //     SenderId = sender.Id,
        //     FriendId = friend.Id
        // };
        // await _context.Friendships.AddAsync(friendship);
        // await _context.SaveChangesAsync();
    }
}