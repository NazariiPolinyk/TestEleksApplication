using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestEleksApplication.DataLayer.Models;
using TestEleksApplication.Interfaces;
using TestEleksApplication.Services.AuthenticationService;

namespace TestEleksApplication.Presentation.Operations
{
    public class UsersApiOperations : ApiOperations, IApiOperations
    {
        public static async Task Post(AuthenticateModel model)
        {
            using (HttpResponseMessage res = await Client.PostAsJsonAsync(BaseUrl + "Users/Token", model))
            {
                res.EnsureSuccessStatusCode();
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    Console.WriteLine(data);
                    if(data != null)
                    {
                        var authenticatedUser = JsonConvert.DeserializeObject<User>(data);
                        Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authenticatedUser.Token);
                    }
                }
            }
        }
    }
}
