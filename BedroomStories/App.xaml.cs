using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace BedroomStories
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "BedStories.db";

        public static StoryRepository database;

        public static StoryRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new StoryRepository(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
