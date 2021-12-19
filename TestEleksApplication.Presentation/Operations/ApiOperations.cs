using System.Net.Http;
using TestEleksApplication.Interfaces;

namespace TestEleksApplication.Presentation.Operations
{
    public class ApiOperations : IApiOperations
    {
        public static HttpClient Client { get; set; } = new HttpClient();
        public static string BaseUrl { get; set; } = "https://localhost:44379/api/";
    }
}
