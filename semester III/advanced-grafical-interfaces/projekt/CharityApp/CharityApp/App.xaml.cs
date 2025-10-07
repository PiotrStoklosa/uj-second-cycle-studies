using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CharityApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {

        public static SuperAdmin SuperAdmin { get; private set; }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            ApplicationLanguages.PrimaryLanguageOverride = "pl-PL";

            InitializeUsers();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private void InitializeUsers()
        {
            SuperAdmin = new SuperAdmin("s", "s");

            var admin1 = new Admin("admin1", "password1", SuperAdmin, "Jan", "Kowalski", "12345678901", "Warszawa");
            var admin2 = new Admin("admin2", "password2", SuperAdmin, "Anna", "Nowak", "98765432109", "Kraków");
            var admin3 = new Admin("admin3", "password3", SuperAdmin, "Marek", "Winnicki", "91327432801", "Gdańsk");
            var admin4 = new Admin("admin4", "password4", SuperAdmin, "Radosław", "Kłaj", "99765455112", "Poznań");
            var admin5 = new Admin("admin4", "password4", SuperAdmin, "Wiktoria", "Kołodziej", "99165545186", "Rzeszów");
            SuperAdmin.AddAdmin(admin1);
            SuperAdmin.AddAdmin(admin2);
            SuperAdmin.AddAdmin(admin3);
            SuperAdmin.AddAdmin(admin4);
            SuperAdmin.AddAdmin(admin5);
            


            DateTime date1 = new DateTime(2020, 1, 1, 12, 0, 0);
            admin1.AddVolunteer(new Volunteer("Joanna Kazimierczak", "Rynek Główny w Krakowie", date1, new UserCredentials("volunteer1", "password1")));
            admin1.AddVolunteer(new Volunteer("Andrzej Rój", "Aleje Banacha", date1, new UserCredentials("volunteer2", "password2")));

            admin2.AddVolunteer(new Volunteer("Katarzyna Chrzan", "Wawel", date1, new UserCredentials("volunteer3", "password3")));
            admin2.AddVolunteer(new Volunteer("Krystian Koneczny", "Kampus PW", date1, new UserCredentials("volunteer4", "password4")));

            SuperAdmin.AddGoal(new Goal("Wsparcie systemu edukacji", "Zakup licencji Visual Studio dla szkół", 30000, "1234 5678"));
            SuperAdmin.AddGoal(new Goal("Wsparcie talentów sportowych", "Ufundowanie stypendium dla młodych obiecujących sportowców", 20000, "1234 5678"));
            SuperAdmin.AddGoal(new Goal("Wsparcie uczelni", "Zakup książek dla bibliotek uczelnianych", 40000, "1234 5678"));
            SuperAdmin.AddGoal(new Goal("Wsparcie osób starszych", "Ufundowanie warsztatów dla seniorów w domach kultury", 10000, "1234 5678"));
            SuperAdmin.AddGoal(new Goal("Wsparcie ochrony zdrowia", "Zakup sprzętu do USG", 15000, "1234 5678"));
        }
    }
}
