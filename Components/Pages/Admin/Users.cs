using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Iskolaryo.Components.Pages.Admin
{
    public class Users
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Role { get; set; }
        public string JoinedClubID { get; set; }
        public Users? User { get; set; }
    }
}
