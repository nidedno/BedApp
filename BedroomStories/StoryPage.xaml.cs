using Xamarin.Forms;
using System;

namespace BedroomStories
{
    public partial class StoryPage : ContentPage
    {
        private string title;
        public string Title { get { return title; } }

        public StoryPage(string title, string text)
        {
            BindingContext = this;
            this.title = title;

            InitializeComponent();
            StoryTitle.Text = title;
            StoryContent.Text = text;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void OnGoodNightClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
