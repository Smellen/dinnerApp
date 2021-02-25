using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DinnerWebApp.Data.Models
{
    public class DinnerDao
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("Owner")]
        public OwnerDao Owner { get; set; }

        [BsonElement("BaseScore")]
        public double BaseScore { get; set; }

        [BsonElement("BonusPoints")]
        public double BonusPoints { get; set; }

        [BsonElement("TotalScore")]
        public double TotalScore { get; set; }
    }
}
