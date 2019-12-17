using System;
namespace HttpApi.Models
{
    public class HtmlDatabaseSettings : IHtmlDatabaseSettings
    {
        public string HtmlCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IHtmlDatabaseSettings
    {
        string HtmlCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
