using SafeShift.Models;

namespace SafeShift.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int userId);
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task<bool> DeleteAsync(int userId);
}
