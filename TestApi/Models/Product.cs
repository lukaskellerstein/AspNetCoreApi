using System;
using System.Collections.Generic;
using System.Security.Claims;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace TestApi.Models
{
    public class Product
    {

        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonElement("id")]
        public string Id { get; set; }  

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("state")]
        public string State { get; set; }
        
        [BsonElement("category")]
        public string Category {get; set;}

        [BsonElement("price")]
        public int Price {get; set;}

    }
}