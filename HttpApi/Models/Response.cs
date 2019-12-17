using System;
namespace HttpApi.Models
{
    public class Response
    {
        public int statusCode { get; set; }
        public string data { get; set; }
        public string country { get; set; }
        public string debug { get; set; }
    }
}
