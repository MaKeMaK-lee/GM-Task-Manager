using GM_Task_Manager.Store.Entities.ToDoTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_Task_Manager.GUI.MVVM_Main_Components.Model
{
    /// <summary>
    /// Сервис, представляющий модель в данной реализации мввм и инкапсулирующий гуи 
    /// </summary>
    public interface IServicesAccessor
    {
        bool SaveDatabase();

        Core.ViewModel GetMainViewModel();//TODO DELETE

        IEnumerable<ToDoTask> GetTodos();

        IEnumerable<ToDoTask> GetTodos(string? text, Store.Entities.ToDoTask.TaskStatus? status,
            string? created, string? updated, string? deadline);

        void RemoveToDoTask(IEnumerable<ToDoTask> toDoTasks);

        void GetStringAndDo(string url, Action<string?> continueAction);

        void AddMany(IEnumerable<ToDoTask> items);

        void Print(IEnumerable<ToDoTask> items);
    }
}
