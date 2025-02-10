using GM_Task_Manager.Endpoints.Database_Contexts;
using GM_Task_Manager.Store.Entities.ToDoTask;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_Task_Manager.Endpoints.Databases
{
    public class ToDoTask_Database : IDisposable
    {
        private bool disposed = false;
        private ToDoTask_DatabaseContext _dbContext;

        public ObservableCollection<ToDoTask> Todos
        {
            private set;
            get;
        }

        public ToDoTask_Database()
        {
            Init();
        }

        private void Init()
        {
            _dbContext = new ToDoTask_DatabaseContext("Tasks Database.sqlite");
            _dbContext.Database.EnsureCreated();
            LoadData();
        }

        private void LoadData()
        {
            _dbContext.ToDoTasks.Load();
            Todos = _dbContext.ToDoTasks.Local.ToObservableCollection();
        }

        private bool SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public void Load()
        {
            LoadData();
        }

        /// <summary>
        /// Сохраняет текущее состояние базы данных в файл
        /// </summary>
        /// <returns>True, если изменения успешно сохранены</returns>
        public bool Save()
        {
            return SaveChanges();
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            _dbContext.Dispose();

            GC.SuppressFinalize(this);
            disposed = true;
        }
    }
}
