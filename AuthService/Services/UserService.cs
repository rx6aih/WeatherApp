using AuthService.DAL.Entities;
using AuthService.DAL.Implementations;
using AuthService.Utility;
using AuthService.Utility.Jwt;
using Permission = AuthService.DAL.Enums.Permission;

namespace AuthService.Services;

public class UserService(
    UserRepository userRepository,
    PasswordHasher passwordHasher,
    JwtProvider provider)
{
    public async Task Register(string userName, string email, string password)
    {
        var hashedPassword = passwordHasher.Generate(password);

        var user = new User(userName, email, hashedPassword);

        await userRepository.Add(user);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await userRepository.GetByEmailAsync(email);
        var result = passwordHasher.Verify(password, user.PasswordHash);
        if (result == false)
            throw new Exception("Failed Login");

        var token = provider.GenerateToken(user);

        return token;
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await userRepository.GetByEmailAsync(email);
        return user;
    }

    public string Validate(string token)
    {
        var user = provider.ValidateToken(token);
        if (user == null)
            return string.Empty;

        return user;
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        return await userRepository.GetAllUsers();
    }

    public Task<HashSet<Permission>> GetPermissions(int userId)
    {
        return userRepository.GetPermissions(userId);
    }
}