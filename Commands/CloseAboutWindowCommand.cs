using EverythingSearch.ViewModel;

namespace EverythingSearch.Commands
{
    public class CloseAboutWindowCommand : CommandBase
    {
        public MainViewModel MainViewModel { get; set; }   

        public CloseAboutWindowCommand(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public override void ExecuteLogic(object? parameter)
        {
            MainViewModel.CloseAboutWindow();
        }
    }
}
