using GM_Task_Manager.GUI.Core;
using GM_Task_Manager.GUI.MVVM_Main_Components.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_Task_Manager.GUI.MVVM_Main_Components.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
        private INavigation _navigation;

        public INavigation Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToTodosListCommand { get; set; }

        public MainViewModel(INavigation navigationService, IServicesAccessor servicesAccessor)
        {

            Navigation = navigationService;
            NavigateToTodosListCommand = new RelayCommand(o =>
            {
                Navigation.NavigateTo<TodosListViewModel>();
            }, o => true);

            Navigation.NavigateTo<TodosListViewModel>();
        }
    }
}
