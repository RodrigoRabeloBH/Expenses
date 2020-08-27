using System.Collections.Generic;

namespace Domain
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // Entity Relation
        public IEnumerable<Expense> Expenses { get; set; }
    }
}