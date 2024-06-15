using Xamarin.Forms;
using System;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace BedroomStories
{
    public partial class CreateStoryPage : ContentPage
    {
        private OpenAIAPI openAI;
        private readonly string apiKey = "YOU_KEY_THERE";
        private bool inProgress;

        public CreateStoryPage()
        {
            InitializeComponent();

            openAI = new OpenAIAPI(apiKey);
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnDreamUpStoryClicked(object sender, EventArgs e)
        {
            if (inProgress)
            {
                return;
            }

            // Validate no null input.
            if (ChildName.Text == null || Language.Text == null || ChildAge.Text == null || GenderPicker.SelectedItem == null || StoryPlace.Text == null)
            {
                await DisplayAlert("Warning!", "Fill all entry.", "OK");
                return;
            }

            string childName = ChildName.Text;
            string language = Language.Text;
            int age = int.Parse(ChildAge.Text);
            string gender = GenderPicker.SelectedItem.ToString();
            string storyPlace = StoryPlace.Text;

            try
            {
                // Показать индикатор активности
                ActivityIndicator.IsRunning = true;
                ActivityIndicator.IsVisible = true;
                Content.IsEnabled = false;

                inProgress = true;
                //GPT request there.
                string requestText = $"Generate a bedtime story for a {gender.ToLower()} child named {childName}, aged {age}, who loves to play in {storyPlace}. The story should be in {language}. Story should have this format: Title:<StoryName> Content:<StoryText>";
            
                var result = await openAI.Chat.CreateChatCompletionAsync(new ChatRequest()
                {
                    Model = Model.ChatGPTTurbo_16k,
                    Temperature = 0.1,
                    Messages = new ChatMessage[] {
                        new ChatMessage(ChatMessageRole.User, requestText)
                    }
                });

                Console.Write(result);

                // Date
                DateTime now = DateTime.Now;
                var date = $"{now.ToString("MMMM")} {now.Day}, {now.Year}";

                var splitResult = result.ToString().Split(new[] { "Content:" }, StringSplitOptions.None);
                // Title
                var title = splitResult[0].Split(new[] { "Title:" }, StringSplitOptions.None)[1];

                // Content
                var content = splitResult[1];

                // Preview
                var upToNCharacters = content.Substring(0, Math.Min(content.Length, 80));

                var story = new Story() { Title = title.Trim(), Date = date, Preview = $"{content.Substring(0, Math.Min(content.Length, 100))}...", Content = content.Trim() };

                App.Database.SaveItem(story);

                ActivityIndicator.IsRunning = false;
                ActivityIndicator.IsVisible = false;
                Content.IsEnabled = true;
                await Navigation.PushAsync(new StoryPage(story.Title, story.Content));
            }
            catch (Exception)
            {
                await DisplayAlert("Warning!", "We have some problems. Try later.", "OK");
            }
            finally
            {
                ActivityIndicator.IsRunning = false;
                ActivityIndicator.IsVisible = false;
                Content.IsEnabled = true;
                inProgress = false;
            }
        }
    }
}
