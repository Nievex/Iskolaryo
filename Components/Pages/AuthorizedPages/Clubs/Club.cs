namespace Iskolaryo.Components.Pages.AuthorizedPages.Clubs
{
    public class Club
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Member Moderator { get; set; }
        public List<Member> Members { get; set; }

    }
}
