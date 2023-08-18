using EverythingSearch.ViewModel;

namespace EverythingSearch.Commands
{
    public class OpenAboutWindowCommand : CommandBase
    {
        public MainViewModel MainViewModel { get; set; }   

        public OpenAboutWindowCommand(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public override void ExecuteLogic(object? parameter)
        {
            MainViewModel.OpenAboutWindow();
        }
    }
}
