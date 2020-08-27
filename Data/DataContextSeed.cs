using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Data
{
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.User.Any())
                {
                    var userData = File.ReadAllText("../Data/user.json");
                    var user = JsonConvert.DeserializeObject<User>(userData);

                    byte[] passwordHash, passwordSalt;

                    CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToUpper();

                    context.User.Add(user);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataContext>();
                logger.LogError(ex.Message);
            }
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}