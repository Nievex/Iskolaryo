using Iskolaryo.Components.Pages.AuthorizedPages.Clubs;

namespace Iskolaryo.Database
{
    public class MemberDatabaseAccess
    {
        private IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        private readonly DatabaseAccess _databaseAccess = new DatabaseAccess();

        public async Task<Member> GetMemberDetails(string username)
        {
            string query = @$"SELECT member.*, club.* FROM users member LEFT JOIN clubs.club_list club ON member.JoinedClubID = club.ID WHERE member.username = '{username}'";
            IEnumerable<Member> memberList = await _databaseAccess.LoadSingleJointData<Member, Club>(query, (member, club) => {
                Console.WriteLine(club.Name);
                Console.WriteLine(club.Description);
                if (club.Name != null)
                {
                    member.Club = club;
                }
                return member;
            }, _config.GetConnectionString("users"), "JoinedClubID");
            return memberList.ToList()[0];
        }

        public async Task<string> GetMemberPassword(string ID)
        {
            string query = @$"SELECT Password FROM users WHERE ID = '{ID}'";
            return await _databaseAccess.LoadData<string, dynamic>(query, new { }, _config.GetConnectionString("users"));
        }

        public async Task UpdateMemberDetails(string ID, string email, string contactNumber, string password)
        {
            string query = @$"UPDATE users SET Email = '{email}', ContactNumber = '{contactNumber}', Password = '{password}' WHERE ID = '{ID}'";
            await _databaseAccess.ExecuteSQL<dynamic>(query, new { }, _config.GetConnectionString("users"));
        }
    }
}
