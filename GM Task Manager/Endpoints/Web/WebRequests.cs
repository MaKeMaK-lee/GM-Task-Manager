using GM_Task_Manager.Store.Entities.ToDoTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GM_Task_Manager.Endpoints.Web
{
    public class WebRequests
    {
        public string? GetString(string url)
        {
            DateTime startingTime = DateTime.Now;

            Task<string>? response = null;
            try
            {
                HttpClient client = new();
                response = client.GetStringAsync(new Uri(url));
                return response.Result;
            }
            catch
            {
                return null;
            }
        }
    }
}
