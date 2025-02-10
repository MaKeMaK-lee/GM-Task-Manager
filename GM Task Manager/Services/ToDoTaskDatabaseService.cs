using GM_Task_Manager.Endpoints.Databases;
using GM_Task_Manager.Store.Entities.ToDoTask;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_Task_Manager.Services
{
    public class ToDoTaskDatabaseService : IToDoTaskDatabaseService
    {
        private bool disposed = false;

        ToDoTask_Database Database { get; set; }

        public ToDoTaskDatabaseService()
        {

            Database = new();
            Database.Load();
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            Database.Dispose();

            GC.SuppressFinalize(this);
            disposed = true;
        }

        public bool Save()
        {
            return Database.Save();
        }

        public void AddMany(IEnumerable<ToDoTask> items)
        {
            foreach (var item in items)
            {
                Database.Todos.Add(item);
            }
        }

        public void Remove(ToDoTask element)
        {
            Database.Todos.Remove(element);
        }

        public IEnumerable<ToDoTask> GetTodos()
        {
            return Database.Todos;
        }

        public IEnumerable<ToDoTask> GetTodos(string? text = null, Store.Entities.ToDoTask.TaskStatus? status = null,
            string? created = null, string? updated = null, string? deadline = null)
        {
            return (from t in Database.Todos
                    where string.IsNullOrEmpty(text) ? true :
                    (t.Name.Contains(text)
                    || t.Description.Contains(text))
                    where status == null ? true :
                    t.Status == status
                    where string.IsNullOrEmpty(created) ? true :
                    t.TimeCreated.ToString("HH:mm dd.MM.yy").Contains(created)
                    where string.IsNullOrEmpty(updated) ? true :
                            t.TimeStatusUpdated.ToString("HH:mm dd.MM.yy").Contains(updated)
                    where string.IsNullOrEmpty(deadline) ? true :
                    t.TimeDeadline.ToString("HH:mm dd.MM.yy").Contains(deadline)
                    select t).ToList();
        }
    }
}
