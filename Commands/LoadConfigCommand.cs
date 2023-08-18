using EverythingSearch.IO;

namespace EverythingSearch.Commands
{
    public class LoadConfigCommand : CommandBase
    {
        public override void ExecuteLogic(object? parameter)
        {
            Saver.LoadConfig();
        }
    }
}
