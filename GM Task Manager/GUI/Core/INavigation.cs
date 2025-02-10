namespace GM_Task_Manager.GUI.Core
{
    public interface INavigation
    {
        ViewModel CurrentView { get; }

        void NavigateTo<T>() where T : ViewModel;
    }
}
