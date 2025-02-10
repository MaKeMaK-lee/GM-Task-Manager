using GM_Task_Manager.GUI.Core;
using GM_Task_Manager.GUI.MVVM_Main_Components.Model;
using GM_Task_Manager.GUI.MVVM_Main_Components.ViewModel;
using GM_Task_Manager.Services;
using GM_Task_Manager.Services.Entity_Management_Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace GM_Task_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow(provider.GetRequiredService<MainViewModel>())
            {
                _servicesAccessor = provider.GetRequiredService<IServicesAccessor>(),
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<TodosListViewModel>(provider => new TodosListViewModel(
                provider.GetRequiredService<IServicesAccessor>(),
                provider.GetRequiredService<IToDoTaskManagementService>(),
                Dispatcher));
            services.AddSingleton<Func<Type, ViewModel>>
                (serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));
            services.AddSingleton<INavigation, Navigation>();

            services.AddSingleton<IServicesAccessor, ServicesAccessor>();

            services.AddSingleton<IToDoTaskDatabaseService, ToDoTaskDatabaseService>();
            services.AddSingleton<IWebAccessService, WebAccessService>();
            services.AddSingleton<IUtilsService, UtilsService>();

            services.AddSingleton<IToDoTaskManagementService, ToDoTaskManagementService>();


            _serviceProvider = services.BuildServiceProvider();




            Startup += Application_Startup;
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

            //((IConvertServiceConnectable)Resources["StringColoroBrushConverter"])
            //    ._ConvertService = _serviceProvider.GetRequiredService<IConvertService>();

            mainWindow.Show();
        }
    }

}
