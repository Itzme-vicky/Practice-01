using crud.Model;

namespace crud.Repository
{
    public interface IUserRepo
    {
        Task<List<User>> GetUserAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> CreatedAsync(User Created);
        Task<bool> UpdateAsync(int id, User Updated);
        Task<bool> DeleteAsync(int id);
    }
}
