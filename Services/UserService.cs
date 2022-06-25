using HokmGame_Server.Data;

namespace HokmGame_Server.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(int id);
    User? GetByName(string name);
    Task<bool> Create(User model);
    Task Delete(int id);
}

public class UserService : IUserService
{
    private AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return _context.Users;
    }

    public async Task<User> GetById(int id)
    {
        return await getUser(id);
    }

    //returns user by name
    public User? GetByName(string name)
    {
        return _context.Users.Where(x => x.Name == name).FirstOrDefault();
    }

    //create a new user
    public async Task<bool> Create(User model)
    {
        // validate
        if (_context.Users.Any(x => x.Name == model.Name))
            return false;

        // hash password
        model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

        // save user
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task Delete(int id)
    {
        var user = await getUser(id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    // helper methods
    private async Task<User> getUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return Task.FromResult(user).Result;
    }
}