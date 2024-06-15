using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;

namespace BedroomStories
{
    public partial class PreviousStoriesPage : ContentPage
    {
        public ObservableCollection<Story> Stories { get; set; }

        public PreviousStoriesPage()
        {
            InitializeComponent();

            var localStories = App.Database.GetItems();
            Stories = new ObservableCollection<Story>();

            foreach (var localStory in localStories)
            {
                localStory.ReadAgainCommand = new Command(() =>
                {
                    ReadAgain(localStory.Title, localStory.Content);
                });

                Stories.Add(localStory);
            }

            StoriesListView.ItemsSource = Stories;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private ViewCell lastCell;
        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.Transparent;
                lastCell = viewCell;
            }
        }

        private async void ReadAgain(string title, string text)
        {
            await Navigation.PushAsync(new StoryPage(title, text));
        }
    }

}
