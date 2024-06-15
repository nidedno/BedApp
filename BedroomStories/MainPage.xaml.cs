using Xamarin.Forms;
using System;

namespace BedroomStories
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCreateStoryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateStoryPage());
        }

        private async void OnViewPreviousStoriesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreviousStoriesPage());
        }

        private async void OnAboutUsClicked(object sender, EventArgs e)
        {
            await DisplayAlert("About Us", "This app helps you create unique stories for your children.", "OK");
        }
    }
}