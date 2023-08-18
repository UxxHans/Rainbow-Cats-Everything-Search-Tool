using EverythingSearch.ViewModel;

namespace EverythingSearch.Commands
{
    public class QuitProgramCommand : CommandBase
    {
        public MainViewModel MainViewModel { get; set; }   

        public QuitProgramCommand(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public override void ExecuteLogic(object? parameter)
        {
            MainViewModel.QuitProgram();
        }
    }
}
