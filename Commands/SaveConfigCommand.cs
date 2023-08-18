using EverythingSearch.ViewModel;
using EverythingSearch.IO;

namespace EverythingSearch.Commands
{
    public class SaveConfigCommand : CommandBase
    {
        public ConfigViewModel ConfigViewModel { get; set; }

        public SaveConfigCommand(ConfigViewModel configViewModel)
        {
            ConfigViewModel = configViewModel;
        }

        public override void ExecuteLogic(object? parameter)
        {
            Saver.SaveConfig();
            new CloseConfigWindowCommand(ConfigViewModel.MainViewModel).Execute(null);
        }
    }
}
