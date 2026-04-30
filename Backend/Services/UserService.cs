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
            if (Preferences.Default.ContainsKey("user_id"))
            {
                Preferences.Default.Remove("user_id");
            }
            
            // 3. Clear any auth tokens if you're using them
            if (Preferences.Default.ContainsKey("auth_token"))
            {
                Preferences.Default.Remove("auth_token");
            }
        }
    }
}