using GM_Task_Manager.Endpoints.Printing;
using GM_Task_Manager.Store.Entities.ToDoTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_Task_Manager.Services
{
    public class UtilsService : IUtilsService
    {
        Printing PrintingTools { get; set; }

        public UtilsService()
        {
            PrintingTools = new();
        }

        public void Print(IEnumerable<ToDoTask> items)
        {
            PrintingTools.Print(items);
        }
    }
}
