using EverythingSearch.ViewModel;

namespace EverythingSearch.Commands
{
    public class OpenConfigWindowCommand : CommandBase
    {
        public MainViewModel MainViewModel { get; set; }   

        public OpenConfigWindowCommand(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public override void ExecuteLogic(object? parameter)
        {
            MainViewModel.OpenConfigWindow();
        }
    }
}
