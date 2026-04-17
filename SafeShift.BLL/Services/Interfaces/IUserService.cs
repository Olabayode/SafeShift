using SafeShift.Models.DTOs.Users;

namespace SafeShift.BLL.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserReadDto>> GetAllAsync();
    Task<UserReadDto?> GetByIdAsync(int userId);
    Task<bool> DeleteAsync(int userId);
}
