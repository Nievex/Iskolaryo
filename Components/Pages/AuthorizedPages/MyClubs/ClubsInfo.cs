namespace Iskolaryo.Components.Pages.AuthorizedPages.MyClubs
{
    public class ClubsInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isFeatured { get; set; }
        public string ModeratorID { get; set; }
        public ClubsInfo? Club { get; set; }
    }
}
