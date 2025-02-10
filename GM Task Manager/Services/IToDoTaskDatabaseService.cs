using GM_Task_Manager.Store.Entities.ToDoTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_Task_Manager.Services
{
    public interface IToDoTaskDatabaseService : IDisposable
    {
        void AddMany(IEnumerable<ToDoTask> items);

        IEnumerable<ToDoTask> GetTodos();

        IEnumerable<ToDoTask> GetTodos(string? text, Store.Entities.ToDoTask.TaskStatus? status,
            string? created, string? updated, string? deadline);

        void Remove(ToDoTask element);

        public bool Save();
    }
}
