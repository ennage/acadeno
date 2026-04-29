using System.Text.RegularExpressions;
using Acadeno.Backend.Models.Admin;
using Acadeno.Backend.Tools;

namespace Acadeno.Backend.Services
{
    public class ValidationService
    {
        private readonly AppDbContext _db;
        public ValidationService(AppDbContext db)
        {
            _db = db;
        }
        
        public (bool IsValid, string Message) ValidateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return (false, "The password field cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                return (false, "The email field cannot be empty.");
            }

            if (!Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return (false, "Is the email formatted correctly.");
            }

            return (true, "The user is valid.");
        }    
    }
}