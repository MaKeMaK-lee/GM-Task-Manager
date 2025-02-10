using GM_Task_Manager.Store.Entities.ToDoTask;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GM_Task_Manager.Services.Entity_Management_Services
{
    public interface IToDoTaskManagementService
    {
        public ToDoTask Create(string name, string description, DateTime deadlineTime, Store.Entities.ToDoTask.TaskStatus? taskStatus = null);

        IEnumerable<ToDoTask> CreateManyFromJsonplaceholderTaskJson(string? json);
    }
}
