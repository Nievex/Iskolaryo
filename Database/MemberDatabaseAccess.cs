using Iskolaryo.Components.Pages.Admin;
using Iskolaryo.Components.Pages.AuthorizedPages.Clubs;
using Iskolaryo.Components.Pages.AuthorizedPages.MyClubs;

namespace Iskolaryo.Database
{
    public class MemberDatabaseAccess
    {
        private IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
        private readonly DatabaseAccess _databaseAccess = new DatabaseAccess();

        public async Task<Member> GetMemberDetailsByUsername(string username)
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

        public async Task<Member> GetMemberDetailsByID(string ID)
        {
            string query = @$"SELECT member.*, club.* FROM users member LEFT JOIN clubs.club_list club ON member.JoinedClubID = club.ID WHERE member.ID = '{ID}'";
            IEnumerable<Member> memberList = await _databaseAccess.LoadSingleJointData<Member, Club>(query, (member, club) =>
            {
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

        public async Task<Member> GetMember(string ID)
        {
            string query = $@"SELECT * FROM users WHERE ID ='{ID}'";
            return await _databaseAccess.LoadData<Member, dynamic>(query, new { }, _config.GetConnectionString("users"));
        }

        // Users
        public async Task<List<Users>> GetUsersAsync()
        {
            string query = @"SELECT * FROM users WHERE Role = 'User'";
            return (await _databaseAccess.LoadDataAsList<Users, object>(query, null, _config.GetConnectionString("users"))).ToList();
        }

        public async Task CreateUserAsync(Users user)
        {
            string insertQuery = @"INSERT INTO users (ID, Username, Password, FirstName, LastName, Email, ContactNumber, Role, JoinedClubID) VALUES (@ID, @Username, @Password, @FirstName, @LastName, @Email, @ContactNumber, @Role, @JoinedClubID)";
            await _databaseAccess.ExecuteSQL(insertQuery, user, _config.GetConnectionString("users"));
        }

        public async Task UpdateUserAsync(Users user)
        {
            string updateQuery = @"UPDATE users SET Username = @Username, Password = @Password, FirstName = @FirstName, LastName = @LastName, Email = @Email, ContactNumber = @ContactNumber, Role = @Role, JoinedClubID = @JoinedClubID WHERE ID = @ID";
            await _databaseAccess.ExecuteSQL(updateQuery, user, _config.GetConnectionString("users"));
        }

        public async Task DeleteUserAsync(string userID)
        {
            string deleteQuery = @"DELETE FROM users WHERE ID = @UserID";
            await _databaseAccess.ExecuteSQL(deleteQuery, new { UserID = userID }, _config.GetConnectionString("users"));
        }

        public async Task<bool> CheckIfUserExistsAsync(string username)
        {
            string query = $"SELECT COUNT(*) FROM users WHERE Username = '{username}'";
            int count = await _databaseAccess.LoadData<int, dynamic>(query, new { }, _config.GetConnectionString("users"));
            return count > 0;
        }

        public async Task<bool> CheckIfUsernameExistsAsync(string ID, string username)
        {
            string query = $"SELECT COUNT(*) FROM users WHERE ID != '{ID}' AND Username = '{username}'";
            int count = await _databaseAccess.LoadData<int, dynamic>(query, new { }, _config.GetConnectionString("users"));
            return count > 0;
        }


        // Clubs
        public async Task<List<Clubs>> GetClubsListAsync()
        {
            string query = @"SELECT * FROM club_list";
            return (await _databaseAccess.LoadDataAsList<Clubs, object>(query, null, _config.GetConnectionString("clubs"))).ToList();
        }

        public async Task CreateClubsAsync(Clubs club)
        {
            string insertQuery = @"INSERT INTO club_list (ID, Name, Description, isFeatured, ModeratorID) VALUES (@ID, @Name, @Description, @isFeatured, @ModeratorID)";
            await _databaseAccess.ExecuteSQL(insertQuery, club, _config.GetConnectionString("clubs"));
        }

        public async Task UpdateClubAsync(Clubs club)
        {
            string updateQuery = @"UPDATE club_list SET Name = @Name, Description = @Description, isFeatured = @isFeatured, ModeratorID = @ModeratorID WHERE ID = @ID";
            await _databaseAccess.ExecuteSQL(updateQuery, club, _config.GetConnectionString("clubs"));
        }

        public async Task DeleteClubAsync(string clubID)
        {
            string deleteQuery = @"DELETE FROM club_list WHERE ID = @ClubID";
            await _databaseAccess.ExecuteSQL<dynamic>(deleteQuery, new { ClubID = clubID }, _config.GetConnectionString("clubs"));
        }

        public async Task<bool> CheckIfClubExistsAsync(string Name)
        {
            string query = $"SELECT COUNT(*) FROM club_list WHERE Name = '{Name}'";
            int count = await _databaseAccess.LoadData<int, dynamic>(query, new { }, _config.GetConnectionString("clubs"));
            return count > 0;
        }

        public async Task<bool> CheckIfClubNameAsync(string ID, string Name)
        {
            string query = $"SELECT COUNT(*) FROM club_list WHERE ID != '{ID}' AND Name = '{Name}'";
            int count = await _databaseAccess.LoadData<int, dynamic>(query, new { }, _config.GetConnectionString("clubs"));
            return count > 0;
        }

        // Admins
        public async Task<List<Admins>> GetAdminsAsync()
        {
            string query = @"SELECT * FROM users WHERE Role = 'Admin'";
            return (await _databaseAccess.LoadDataAsList<Admins, object>(query, null, _config.GetConnectionString("users"))).ToList();
        }

        public async Task CreateAdminAsync(Admins admin)
        {
            string insertQuery = @"INSERT INTO users (ID, Username, Password, FirstName, LastName, Email, ContactNumber, Role, JoinedClubID) VALUES (@ID, @Username, @Password, @FirstName, @LastName, @Email, @ContactNumber, @Role, @JoinedClubID)";
            await _databaseAccess.ExecuteSQL(insertQuery, admin, _config.GetConnectionString("users"));
        }

        public async Task UpdateAdminAsync(Admins admin)
        {
            string updateQuery = @"UPDATE users SET Username = @Username, Password = @Password, FirstName = @FirstName, LastName = @LastName, Email = @Email, ContactNumber = @ContactNumber, Role = @Role, JoinedClubID = @JoinedClubID WHERE ID = @ID";
            await _databaseAccess.ExecuteSQL(updateQuery, admin, _config.GetConnectionString("users"));
        }

        public async Task DeleteAdminAsync(string userID)
        {
            string deleteQuery = @"DELETE FROM users WHERE ID = @UserID AND Role = 'Admin'";
            await _databaseAccess.ExecuteSQL<dynamic>(deleteQuery, new { UserID = userID }, _config.GetConnectionString("users"));
        }

        public async Task<bool> CheckIfAdminExistsAsync(string username)
        {
            string query = $"SELECT COUNT(*) FROM users WHERE Username = '{username}'";
            int count = await _databaseAccess.LoadData<int, dynamic>(query, new { }, _config.GetConnectionString("users"));
            return count > 0;
        }

        public async Task<bool> CheckIfAdminNameExistsAsync(string ID, string username)
        {
            string query = $"SELECT COUNT(*) FROM users WHERE ID != '{ID}' AND Username = '{username}'";
            int count = await _databaseAccess.LoadData<int, dynamic>(query, new { }, _config.GetConnectionString("users"));
            return count > 0;
        }

        // ClubURL
        public async Task<List<ClubsInfo>> GetClubsURLAsync()
        {
            string query = @"SELECT * FROM club_list";
            return (await _databaseAccess.LoadDataAsList<ClubsInfo, object>(query, null, _config.GetConnectionString("clubs"))).ToList();
        }
    }
}
