namespace CSharp.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Models;


public interface IUserAuthenticateService
{
    Task<User> Authenticate(string username, string password);
    Task<IEnumerable<User>> GetAll();
}

public class UserAuthenticateService : IUserAuthenticateService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<User> _users = new List<User>
    {
        new User { Id = 1, Username = "Test1", Password = "P@55w0rd1" }
    };

    public async Task<User> Authenticate(string username, string password)
    {
        // wrapped in "await Task.Run" to mimic fetching user from a db
        var user = await Task.Run(() => _users.FirstOrDefault(x => x.Username == username && x.Password == password));

        // on auth fail: null is returned because user is not found
        // on auth success: user object is returned
        return user;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        // wrapped in "await Task.Run" to mimic fetching users from a db
        return await Task.Run(() => _users);
    }
}