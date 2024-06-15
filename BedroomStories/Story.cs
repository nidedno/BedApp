using System.Collections.ObjectModel;
using Xamarin.Forms;
using SQLite;

namespace BedroomStories
{
    [Table("Stories")]
    public class Story
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public string Preview { get; set; }

        public string Content { get; set; }

        [Ignore]
        public Command ReadAgainCommand { get; set; }
    }
}
