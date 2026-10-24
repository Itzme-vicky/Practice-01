using crud.Data;
using crud.Model;
using Microsoft.EntityFrameworkCore;

namespace crud.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUserAsync()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return users;
        }
        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            return user;
        }

      
        public async Task<User> CreatedAsync(User Created)
        {
            _context.Users.Add(Created);
            await _context.SaveChangesAsync();
            return Created;
        }
        public async Task<bool> UpdateAsync(int id, User Updated)
        {
            var existing=await _context.Users.FindAsync(id);
            if(existing is null) return false;

            existing.Name = Updated.Name;
            existing.Email = Updated.Email;
            existing.Password = Updated.Password;

            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing is null) return false;

            _context.Users.Remove(existing);
            await _context.SaveChangesAsync();
            return true;

        }

        

      
    }
}
