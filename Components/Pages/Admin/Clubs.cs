namespace Iskolaryo.Components.Pages.Admin
{
    public class Clubs
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isFeatured { get; set; }
        public string ModeratorID { get; set; }
        public Clubs? Club { get; set; }
    }
}
