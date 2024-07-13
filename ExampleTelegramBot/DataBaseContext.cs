using ExampleTelegramBot.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ExampleTelegramBot
{
    internal class DataBaseContext : DbContext
    {
        public DbSet<UserData> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            Settings.Current = JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText(FilesManager.SettingsFile));
            optionsBuilder.UseNpgsql($"{Settings.Current.ConnectionString}");
        }
        public List<long> GetAllUser()
        {
            var result = Users.Select(x => x.TelegramId).ToList();

            return result;
        }
        public void Update(UserData user)
        {
            Users.Update(user);
            SaveChanges();
        }
        public void Add(UserData user)
        {
            Users.Add(user);
            SaveChanges();
        }
        public UserData GetById(long id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }
        public UserData GetByTgId(long id)
        {
            return Users.FirstOrDefault(u => u.TelegramId == id);
        }

        public UserData GetByUsername(string username)
        {
            return Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
