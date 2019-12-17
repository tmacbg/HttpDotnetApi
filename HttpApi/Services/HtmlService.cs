using System;
using System.Collections.Generic;
using HttpApi.Models;
using MongoDB.Driver;

namespace HttpApi.Services
{
    public class HtmlService
    {
        public readonly IMongoCollection<Html> _html;


        public HtmlService(IHtmlDatabaseSettings settings)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("TermsAndConditions");

            _html = db.GetCollection<Html>("Html"); 
        }

        public List<Html> Get() =>
            _html.Find(html => true).ToList();

        public Html Get(string id) =>
            _html.Find(html => html.Id == id).FirstOrDefault();

        public Html Create(Html html)
        {
            _html.InsertOne(html);
            return html;
        }

        // 
        public Html GetSource(string filter) =>
            _html.Find(filter).FirstOrDefault();
    }
}
