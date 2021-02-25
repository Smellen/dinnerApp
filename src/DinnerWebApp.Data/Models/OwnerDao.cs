using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DinnerWebApp.Data.Models
{
    public class OwnerDao
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Description")]
        public string Name { get; set; }
    }
}
