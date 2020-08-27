using System;
using System.Threading.Tasks;
using Domain;
using Domain.Dto;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Data
{
    public class UserRepository : Repository<User>, IUserServices
    {
        public UserRepository(DataContext context) : base(context) { }

        public async Task<User> Login(UserToLogin userToLogin)
        {
            var user = await _context.User.FirstAsync(u => u.Username == userToLogin.Username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(userToLogin.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user)
        {
            try
            {
                byte[] passwordHash, passwordSalt;

                CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<User> ResetPassword(UserToLogin userToLogin)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UserExistes(string username)
        {
            User user = await _context.User.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return false;
            else
                return true;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computerHash.Length; i++)
                {
                    if (computerHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
    }
}