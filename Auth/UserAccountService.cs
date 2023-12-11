using Iskolaryo.Database;

namespace Iskolaryo.Auth
{
    public class UserAccountService
    {
        private IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        private readonly DatabaseAccess _databaseAccess = new DatabaseAccess();
        public List<UserAccount> _userAccounts;
        
        public UserAccountService()
        {
            _userAccounts = _databaseAccess.LoadDataAsList<UserAccount, dynamic>("SELECT * FROM users", new { }, _config.GetConnectionString("users")).Result.ToList();
        }

        public UserAccount? GetUserAccount(string username)
        {
            return _userAccounts.FirstOrDefault(u => u.Username == username);
        }
    }
}
