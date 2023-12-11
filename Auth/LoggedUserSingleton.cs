using Iskolaryo.Components.Pages.AuthorizedPages.Clubs;

namespace Iskolaryo.Auth
{
    public class LoggedUserSingleton
    {
        private static Member _loggedUser;

        public static Member LoggedUser
        {
            get { return _loggedUser; }
            set { _loggedUser = value; }
        }

        public static void RemoveLoggedUser()
        {
            LoggedUser = null;
        }
        
    }
}
