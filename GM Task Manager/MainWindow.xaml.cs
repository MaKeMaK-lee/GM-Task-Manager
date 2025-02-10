
using GM_Task_Manager.GUI.Core;
using GM_Task_Manager.GUI.MVVM_Main_Components.Model;
using GM_Task_Manager.GUI.MVVM_Main_Components.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GM_Task_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IServicesAccessor _servicesAccessor;

        public MainWindow(ViewModel mainDataContext)
        {
            InitializeComponent();
            MainView.DataContext = mainDataContext;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if (_servicesAccessor.SaveDatabase())
                e.Cancel = false;
            else
            {
                var result = MessageBox.Show("Данные не удалось сохранить.\n\nВсё равно выйти?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                    return;
                }
            }
        }
    }
}