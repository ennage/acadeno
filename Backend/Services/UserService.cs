namespace Acadeno.Backend.Services
{
    public class UserService
    {
        // This holds the actual User object from your DB
        public Acadeno.Backend.Models.User? CurrentUser {get; set;}
        public bool IsLoggedIn => CurrentUser != null;

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}