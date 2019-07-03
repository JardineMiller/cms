using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using cms.Data_Layer.Models;
using Microsoft.Extensions.Logging;

namespace cms.Data_Layer.Seeding
{
    public class DatabaseSeeder : ISeeder
    {
        private readonly ApplicationDbContext cnt;
        private readonly ILogger<DatabaseSeeder> logger;

        public DatabaseSeeder(ApplicationDbContext cnt, ILogger<DatabaseSeeder> logger)
        {
            this.cnt = cnt;
            this.logger = logger;
        }

        public void SeedAll()
        {
            var timer = new Stopwatch();

            logger.LogInformation("Starting seed...");

            seedPeople();

            logger.LogInformation($"Seeding of database complete. It took {timer.ElapsedMilliseconds} ms");
        }

        private void seedPeople()
        {
            if (!cnt.Users.Any())
            {
                var people = new List<User>()
                {
                    new User() {Name = "Jardine", Email = "jardine@emailprovider.com"},
                    new User() {Name = "Julia", Email = "julia@emailprovider.com"},
                    new User() {Name = "Matt", Email = "matt@emailprovider.com"},
                    new User() {Name = "Alison", Email = "alison@emailprovider.com"},
                };

                cnt.Users.AddRange(people);
                cnt.SaveChanges();
            }
        }
    }
}