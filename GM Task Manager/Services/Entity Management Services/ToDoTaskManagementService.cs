using GM_Task_Manager.Store.Entities.ToDoTask;
using GM_Task_Manager.TrdParty.Entities.JsonAuto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GM_Task_Manager.Services.Entity_Management_Services
{
    public class ToDoTaskManagementService : IToDoTaskManagementService
    {
        public ToDoTask Create(string name, string description, DateTime deadlineTime, Store.Entities.ToDoTask.TaskStatus? taskStatus = null)
        {
            var timeNow = DateTime.Now;
            return new()
            {
                Id = Guid.NewGuid(),
                Status = taskStatus ?? Store.Entities.ToDoTask.TaskStatus.Created,
                TimeCreated = timeNow,
                TimeStatusUpdated = timeNow,

                Name = name,
                Description = description,
                TimeDeadline = deadlineTime,
            };
        }

        public IEnumerable<ToDoTask> CreateManyFromJsonplaceholderTaskJson(string? json)
        {
            if (json == null)
                return [];
            var jsonplaceholderTasks = JsonSerializer.Deserialize<IEnumerable<JsonplaceholderTask>>(json);
            var toDoTasks = jsonplaceholderTasks.Select(jt => Create(
                jt.title,
                "This task was imported from afar",
                DateTime.MaxValue,
                jt.completed ? Store.Entities.ToDoTask.TaskStatus.Completed : null));
            return toDoTasks.ToList();

        }
    }
}
