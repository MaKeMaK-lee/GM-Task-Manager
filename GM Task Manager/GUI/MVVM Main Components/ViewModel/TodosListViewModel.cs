using GM_Task_Manager.GUI.Core;
using GM_Task_Manager.GUI.MVVM_Main_Components.Model;
using GM_Task_Manager.Services.Entity_Management_Services;
using GM_Task_Manager.Store.Entities.ToDoTask;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace GM_Task_Manager.GUI.MVVM_Main_Components.ViewModel
{
    public class TodosListViewModel : Core.ViewModel
    {
        private readonly IServicesAccessor _servicesAccessor;

        private readonly IToDoTaskManagementService _toDoTaskManagementService;

        private Dispatcher _currentDispatcher;

        public Dispatcher CurrentDispatcher
        {
            get => _currentDispatcher;
            set
            {
                _currentDispatcher = value;
                OnPropertyChanged(nameof(CurrentDispatcher));
            }
        }

        private IEnumerable<ToDoTask> todos;

        public IEnumerable<ToDoTask> Todos
        {
            get
            {
                return todos;
            }
        }

        private Store.Entities.ToDoTask.TaskStatus? statusFilter;

        public Store.Entities.ToDoTask.TaskStatus? StatusFilter
        {
            get => statusFilter;
            set
            {
                statusFilter = value;
            }
        }

        /// <summary>
        /// Свойтво, возвращающее отфильтрованный список. Все обновления фильтров вызывают OnPropertyChanged для него.
        /// </summary>
        public IEnumerable<ToDoTask> FilteredTodos
        {
            get
            {
                if (StatusFilter == null &&
                    string.IsNullOrEmpty(FilterText_Text) &&
                    string.IsNullOrEmpty(FilterText_TimeCreated) &&
                    string.IsNullOrEmpty(FilterText_TimeUpdated) &&
                    string.IsNullOrEmpty(FilterText_TimeDeadline))
                    return Todos;
                return _servicesAccessor.GetTodos(FilterText_Text, StatusFilter,
            FilterText_TimeCreated, FilterText_TimeUpdated, FilterText_TimeDeadline);
            }
        }

        private string filterText_Text;

        public string FilterText_Text
        {
            get => filterText_Text;
            set
            {
                filterText_Text = value;
                OnPropertyChanged(nameof(FilterText_Text));
            }
        }

        private string filterText_TimeCreated;

        public string FilterText_TimeCreated
        {
            get => filterText_TimeCreated;
            set
            {
                filterText_TimeCreated = value;
                OnPropertyChanged(nameof(FilterText_TimeCreated));
            }
        }

        private string filterText_TimeUpdated;

        public string FilterText_TimeUpdated
        {
            get => filterText_TimeUpdated;
            set
            {
                filterText_TimeUpdated = value;
                OnPropertyChanged(nameof(FilterText_TimeUpdated));
            }
        }

        private string filterText_TimeDeadline;

        public string FilterText_TimeDeadline
        {
            get => filterText_TimeDeadline;
            set
            {
                filterText_TimeDeadline = value;
                OnPropertyChanged(nameof(FilterText_TimeDeadline));
            }
        }

        public bool IsSelectedAnyItemInToDoTaskCollection
        {
            get
            {
                if (SelectedToDoTasks == null)
                    return false;
                else
                    return true;
            }
        }

        public IEnumerable<ToDoTask>? selectedToDoTasks;

        public IEnumerable<ToDoTask>? SelectedToDoTasks
        {
            get => selectedToDoTasks;
            set
            {
                selectedToDoTasks = value;

                OnPropertyChanged(nameof(IsSelectedAnyItemInToDoTaskCollection));
            }
        }

        public RelayCommand PrintCommand { get; set; }

        public RelayCommand SyncCommand { get; set; }

        public RelayCommand ResetFilters { get; set; }

        public RelayCommand UpdateFilterCommand { get; set; }

        public RelayCommand DataGridSelectionChangedCommand { get; set; }

        public RelayCommand AddToDoTaskCommand { get; set; }

        public RelayCommand RemoveToDoTaskCommand { get; set; }


        private void SetCommands()
        {
            PrintCommand = new RelayCommand(o =>
            {
                var DataItems = FilteredTodos.ToList();
                _servicesAccessor.Print(DataItems);
            }, o => true);
            SyncCommand = new RelayCommand(o =>
            {

                _servicesAccessor.GetStringAndDo("https://jsonplaceholder.typicode.com/todos", (string? json) =>
                {
                    CurrentDispatcher.Invoke(() =>
                    {
                        var items = _toDoTaskManagementService.CreateManyFromJsonplaceholderTaskJson(json);
                        _servicesAccessor.AddMany(items.Where(newItem => !Todos
                        .Any(exists => exists.Name == newItem.Name)));
                        OnPropertyChanged(nameof(FilteredTodos));
                    });
                });
            }, o => true);
            AddToDoTaskCommand = new RelayCommand(o =>
            {
                _servicesAccessor.AddMany([_toDoTaskManagementService.Create("What to do?","",
                        DateTime.Now+TimeSpan.FromDays(7))]);
                ;
                OnPropertyChanged(nameof(FilteredTodos));
            }, o => true);
            RemoveToDoTaskCommand = new RelayCommand(o =>
            {
                if (SelectedToDoTasks != null)
                    _servicesAccessor.RemoveToDoTask(SelectedToDoTasks.ToList());
                OnPropertyChanged(nameof(FilteredTodos));
            }, o => true);
            DataGridSelectionChangedCommand = new RelayCommand(o =>
            {
                if (o == null)
                    SelectedToDoTasks = null;
                else
                {
                    SelectedToDoTasks = ((IEnumerable<object>)o).Cast<ToDoTask>();
                }
            }, o => true);
            ResetFilters = new RelayCommand(o =>
            {
                FilterText_Text = string.Empty;
                FilterText_TimeCreated = string.Empty;
                FilterText_TimeUpdated = string.Empty;
                FilterText_TimeDeadline = string.Empty;
                StatusFilter = null;
                OnPropertyChanged(nameof(FilteredTodos));
            }, o => true);
            UpdateFilterCommand = new RelayCommand(o =>
            {
                OnPropertyChanged(nameof(FilteredTodos));
            }, o => true);
        }

        public TodosListViewModel(IServicesAccessor servicesAccessor, IToDoTaskManagementService toDoTaskManagementService, Dispatcher dispatcher)
        {
            CurrentDispatcher = dispatcher;
            _servicesAccessor = servicesAccessor;
            _toDoTaskManagementService = toDoTaskManagementService;

            SetCommands();

            todos = _servicesAccessor.GetTodos();

        }
    }
}
