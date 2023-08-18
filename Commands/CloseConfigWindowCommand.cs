using EverythingSearch.ViewModel;

namespace EverythingSearch.Commands
{
    public class CloseConfigWindowCommand : CommandBase
    {
        public MainViewModel MainViewModel { get; set; }   

        public CloseConfigWindowCommand(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public override void ExecuteLogic(object? parameter)
        {
            MainViewModel.CloseConfigWindow();
        }
    }
}
