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
    }
}
