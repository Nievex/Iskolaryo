using Iskolaryo.Components.Pages.AuthorizedPages.Clubs;
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

        public async Task<Member> GetMemberDetails(string username)
        {
            var query = @$"SELECT member.*, club.* FROM users member LEFT JOIN clubs.club_list club ON member.JoinedClubID = club.ID WHERE member.username = '{username}'";
            var memberList = await _databaseAccess.LoadSingleJointData<Member, Club>(query, (member, club) => {
                Console.WriteLine(club.Name);
                Console.WriteLine(club.Description);
                if(club.Name != null)
                {
                    member.Club = club;
                }
                return member;
            }, _config.GetConnectionString("users"), "JoinedClubID");
            return memberList.ToList()[0];
        }
    }
}
