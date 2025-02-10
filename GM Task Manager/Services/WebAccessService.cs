
using GM_Task_Manager.Endpoints.Web;
using GM_Task_Manager.Store.Entities.ToDoTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_Task_Manager.Services
{
    public class WebAccessService : IWebAccessService
    {
        WebRequests WebRequests { get; set; }

        public WebAccessService()
        {
            WebRequests = new();
        }

        public void GetStringAndDo(string url, Action<string?> continueAction)
        {
            Task.Run(() =>
            {
                return WebRequests.GetString("https://jsonplaceholder.typicode.com/todos");
            }).ContinueWith((t) => continueAction.Invoke(t.Result));
        }
    }
}
