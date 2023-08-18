using System.IO;
using System.Diagnostics;
using System.Windows;

namespace EverythingSearch.Commands
{
    public class OpenFileInExplorerCommand : CommandBase
    {
        public string Path { get; set; }

        public OpenFileInExplorerCommand(string path)
        {
            Path = path;
        }

        public override void ExecuteLogic(object? parameter)
        {
            if(File.Exists(Path) || Directory.Exists(Path))
            {
                Process.Start("explorer.exe", "/select," + Path);
            }
            else
            {
                MessageBox.Show($"{Path} 路径不存在，文件已经消失", "通知", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
