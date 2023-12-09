namespace Iskolaryo.Auth
{
    public class UserAccountService
    {
        private List<UserAccount> _userAccounts =
        [
            new UserAccount { Username = "admin", Password = "admin", Role = "Admin" },
            new UserAccount { Username = "user", Password = "user", Role = "User" },
            new UserAccount { Username = "guest", Password = "guest", Role = "Guest"}
        ];

        public UserAccount? GetUserAccount(string username)
        {
            return _userAccounts.FirstOrDefault(u => u.Username == username);
        }
    }
}
