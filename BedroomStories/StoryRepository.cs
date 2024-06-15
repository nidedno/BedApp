using System;
using System.Collections.Generic;
using SQLite;

namespace BedroomStories
{
    public class StoryRepository
    {
        private SQLiteConnection database;

        public StoryRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Story>();
        }

        public IEnumerable<Story> GetItems()
        {
            return database.Table<Story>().ToList();
        }

        public Story GetItem(int id)
        {
            return database.Get<Story>(id);
        }

        public int DeleteItem(int id)
        {
            return database.Delete<Story>(id);
        }

        public int SaveItem(Story item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
