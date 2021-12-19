using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TestEleksApplication.DataLayer.Models;
using TestEleksApplication.Interfaces;

namespace TestEleksApplication.Presentation.Operations
{
    public class StudentsApiOperations : ApiOperations, IApiOperations
    {
        public static async Task<IEnumerable<Student>> Get()
        {
                using (HttpResponseMessage res = await Client.GetAsync(BaseUrl + "Students"))
                {
                    res.EnsureSuccessStatusCode();
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if(data != null)
                        {
                            return JsonConvert.DeserializeObject<List<Student>>(data);
                        }
                    }
                }
            return null;
        }
        public static async Task<Student> Get(int id)
        {
                using (HttpResponseMessage res = await Client.GetAsync(BaseUrl + $"Students/{id}"))
                {
                    res.EnsureSuccessStatusCode();
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return JsonConvert.DeserializeObject<Student>(data);
                        }
                    }
                }
            return null;
        }
        public static async Task Post(Student student)
        {
                using (HttpResponseMessage res = await Client.PostAsJsonAsync(BaseUrl + "Students", student)) 
                {
                    res.EnsureSuccessStatusCode();
                }
        }
        public static async Task Put(Student student)
        {
                using (HttpResponseMessage res = await Client.PutAsJsonAsync(BaseUrl + $"Students/{student.Id}", student)) 
                {
                    res.EnsureSuccessStatusCode();
                }
        }
        public static async Task Delete(int id)
        {
                using (HttpResponseMessage res = await Client.DeleteAsync(BaseUrl + $"Students/{id}")) { }
        }
    }
}
