using System.Threading.Tasks;
using Domain;
using Domain.Dto;

namespace Services.Interfaces
{
    public interface IUserServices : IRepository<User>
    {
        Task<User> Register(User user);
        Task<User> Login(UserToLogin userToLogin);
        Task<bool> UserExistes(string username);
        Task<User> ResetPassword(UserToLogin userToLogin);
    }
}