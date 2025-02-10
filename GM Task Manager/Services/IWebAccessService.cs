using GM_Task_Manager.Store.Entities.ToDoTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_Task_Manager.Services
{
    public interface IWebAccessService
    {
        void GetStringAndDo(string url, Action<string?> continueAction);
    }
}
