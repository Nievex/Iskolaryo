namespace Iskolaryo.Components.Pages.AuthorizedPages.Clubs
{
    public class Member
    {
        public string ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name => $"{FirstName} {LastName}";

        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Role { get; set; }

    }
}
