using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HttpApi.Models
{
    public class Html
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string url { get; set; }

        public string selector { get; set; }

        public string rawHtml { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }
    }
}
