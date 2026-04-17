using Microsoft.EntityFrameworkCore;
using SafeShift.DAL.Data;
using SafeShift.DAL.Repositories.Interfaces;
using SafeShift.Models;

namespace SafeShift.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SafeShiftDbContext _context;

    public UserRepository(SafeShiftDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .Include(user => user.Incidents)
            .Include(user => user.Inspections)
            .Include(user => user.Shifts)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(user => user.Incidents)
            .Include(user => user.Inspections)
            .Include(user => user.Shifts)
            .FirstOrDefaultAsync(user => user.UserId == userId);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.UserId);
        if (existingUser is null)
        {
            return null;
        }

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.PasswordHash = user.PasswordHash;
        existingUser.Role = user.Role;

        await _context.SaveChangesAsync();

        return existingUser;
    }

    public async Task<bool> DeleteAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
}
