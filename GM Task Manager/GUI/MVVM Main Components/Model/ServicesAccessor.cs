
using GM_Task_Manager.GUI.MVVM_Main_Components.ViewModel;
using GM_Task_Manager.Services;
using GM_Task_Manager.Store.Entities.ToDoTask;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_Task_Manager.GUI.MVVM_Main_Components.Model
{
    public class ServicesAccessor(
        Func<Type, Core.ViewModel> viewModelFactory,
        IToDoTaskDatabaseService _toDoTaskDatabaseService,
        IUtilsService _utilsService,
        IWebAccessService _webAccessService
        ) : IServicesAccessor
    {
        public Core.ViewModel GetMainViewModel()
        {
            return viewModelFactory.Invoke(typeof(MainViewModel));
        }

        public bool SaveDatabase()
        {
            return _toDoTaskDatabaseService.Save();
        }

        public IEnumerable<ToDoTask> GetTodos()
        {
            return _toDoTaskDatabaseService.GetTodos();
        }

        public IEnumerable<ToDoTask> GetTodos(string? text, Store.Entities.ToDoTask.TaskStatus? status, string? created, string? updated, string? deadline)
        {
            return _toDoTaskDatabaseService.GetTodos(text, status, created, updated, deadline);
        }

        public void AddMany(IEnumerable<ToDoTask> items)
        {
            _toDoTaskDatabaseService.AddMany(items);
        }

        public void RemoveToDoTask(IEnumerable<ToDoTask> items)
        {
            foreach (var item in items)
            {
                _toDoTaskDatabaseService.Remove(item);
            }
            _toDoTaskDatabaseService.Save();
        }

        public void GetStringAndDo(string url, Action<string?> continueAction)
        {
            _webAccessService.GetStringAndDo(url, continueAction);
        }

        public void Print(IEnumerable<ToDoTask> items)
        {
            _utilsService.Print(items);
        }
    }
}
