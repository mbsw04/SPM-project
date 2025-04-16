using dotnetTest.Data;
using dotnetTest.Models;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;

namespace dotnetTest.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbContext context)
        {
            _users = context.GetCollection<User>("users");
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
        
        public async Task<String> GetUserIdByUsernameAsync(string username)
        {
            var user = await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null)
            {
                return "";
            }

            return user.Id; // returns the ObjectId of the user, or an empty ObjectId if no user is found
        }

        // Method to get ObjectId by email
        
        public async Task<String> GetUserIdByEmailAsync(string email)
        {
            var user = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return "";
            }

            return user.Id;
        }
        // Using Entity Framework Core
        public async Task<List<User>> SearchAsync(string searchTerm)
        {
            return await _users.Find(u => u.Username.Contains(searchTerm)).ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            await _users.ReplaceOneAsync(filter, user);
        }
    }
} 