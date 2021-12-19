using System.Net.Http;

namespace TestEleksApplication.Interfaces
{
    public interface IApiOperations
    {
        public static HttpClient Client { get; set; }
        public static string BaseUrl { get; set; }
    }
}
