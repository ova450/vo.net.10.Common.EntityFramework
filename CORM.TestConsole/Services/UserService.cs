using CORM.Core.Repository;
using CORM.TestConsole.Entities;

namespace CORM.TestConsole.Services;

public class UserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepositoryAsync<User> _userRepository;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = unitOfWork.RepositoryAsync<User>();
    }

    public async Task<User?> GetUserAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> CreateUserAsync(string name, string email)
    {
        var user = new User 
        { 
            Name = name,
            Email = email
            // CreatedAt будет установлен автоматически в конструкторе
        };
        
        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        // Проверяем, существует ли пользователь
        var existingUser = await _userRepository.GetByIdAsync(user.Id);
        if (existingUser == null)
            throw new InvalidOperationException($"User with ID {user.Id} not found");
        
        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
        
        return user;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var result = await _userRepository.RemoveAsync(id);
        if (result)
            await _unitOfWork.SaveChangesAsync();
        
        return result;
    }

    public async Task<User?> GetUserWithOrdersAsync(int id)
    {
        var users = await _userRepository.FindAsync(u => u.Id == id);
        return users.FirstOrDefault();
    }
}