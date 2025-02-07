﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinnerWebApp.Data.Models;
using MongoDB;
using MongoDB.Libmongocrypt;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DinnerWebApp.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly IMongoCollection<DinnerDao> _dinners;
        private readonly IMongoCollection<OwnerDao> _owners;
        private const string DinnerCollection = "Dinners";
        private const string OwnersCollection = "Owners";

        public DataRepository(IMongoClient client, string environment)
        {
            IMongoDatabase database = client.GetDatabase(environment);
            _dinners = database.GetCollection<DinnerDao>(DinnerCollection);
            _owners = database.GetCollection<OwnerDao>(OwnersCollection);
        }

        public async Task<DinnerDao> Add(DinnerDao dinner)
        {
            dinner.Id = ObjectId.GenerateNewId().ToString();
            await _dinners.InsertOneAsync(dinner);

            return dinner;
        }

        public async Task<List<DinnerDao>> GetDinners(int skip, int take)
        {
            List<DinnerDao> dinners = await _dinners.Find(_ => true).Skip(skip).Limit(take).SortByDescending(e => e.Date).ToListAsync();

            if (dinners == null || !dinners.Any())
            {
                return null;
            }

            return dinners;
        }

        public async Task<List<OwnerDao>> GetOwners(string id)
        {
            List<OwnerDao> owners = new List<OwnerDao>();
            if (string.IsNullOrWhiteSpace(id))
            {
                owners = await _owners.Find(_ => true).ToListAsync();
            }
            else
            {
                owners = await _owners.Find(e => e.Id == id).ToListAsync();
            }

            if (owners == null || !owners.Any())
            {
                return null;
            }

            return owners;
        }

        public async Task<List<DinnerDao>> Search(DateTime date)
        {
            List<DinnerDao> dinners = await _dinners.Find(e => e.Date == date).ToListAsync();

            if (dinners == null || !dinners.Any())
            {
                return null;
            }

            return dinners;
        }

        public async Task<OwnerDao> Add(OwnerDao owner)
        {
            owner.Id = ObjectId.GenerateNewId().ToString();
            await _owners.InsertOneAsync(owner);

            return owner;
        }

        public async Task<bool> DeleteDinner(DateTime date)
        {
            var deleteResult = await _dinners.DeleteOneAsync(e => e.Date == date);

            return deleteResult.DeletedCount >= 1;
        }

        public async Task<DinnerDao> BestRated()
        {
            var sort = Builders<DinnerDao>.Sort.Descending("TotalScore");
            var bestRated = await _dinners.Find(_ => true).Sort(sort).FirstAsync();

            return bestRated;
        }

        public async Task<double> AverageDinnerScore()
        {
            var total = 0.0;
            var count = await _dinners.CountDocumentsAsync(new BsonDocument());

            await _dinners.Find(_ => true).ForEachAsync(e => total += e.TotalScore);

            var average = total / count;

            return average;
        }

        public async Task<double> AveragePerOwner(string ownerId)
        {
            var dinnersForOwner = await _dinners.Find(e => e.Owner == ownerId).ToListAsync();

            var TotalScores = dinnersForOwner.Sum(e => e.TotalScore);

            return TotalScores / dinnersForOwner.Count;

        }

        public async Task<int> DinnerCount()
        {
            return Convert.ToInt32(await _dinners.CountDocumentsAsync(new BsonDocument()));
        }

        public async Task<bool> HealthCheck()
        {
            OwnerDao something;
            try
            {
                something = await _owners.Find(_ => true).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return something != null;
        }
    }
}
