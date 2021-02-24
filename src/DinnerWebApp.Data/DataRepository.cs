using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinnerWebApp.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DinnerWebApp.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly IMongoCollection<DinnerDao> _dinners;
        private const string DinnerCollection = "Dinner";

        public DataRepository(IMongoClient client, string environment)
        {
            IMongoDatabase database = client.GetDatabase(environment);
            _dinners = database.GetCollection<DinnerDao>(DinnerCollection);
        }

        public async Task<DinnerDao> Add(DinnerDao dinner)
        {
            dinner.Id = ObjectId.GenerateNewId().ToString();
            await _dinners.InsertOneAsync(dinner);

            return dinner;
        }

        public async Task<List<DinnerDao>> GetAllDinners()
        {
            List<DinnerDao> dinners = await _dinners.Find(_ => true).ToListAsync();

            if (dinners == null || !dinners.Any())
            {
                return null;
            }

            return dinners;
        }
    }
}
