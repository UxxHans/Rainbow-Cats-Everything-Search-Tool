using System;
using System.Windows.Input;
using EverythingSearch.Commands;
using EverythingSearch.Service;

namespace EverythingSearch.ViewModel
{
    public class ResultViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; set; }
        public EverythingService.Result Result { get; set; }

        public string Size => FormatByteSize(Result.ByteSize);
        public string DateModified => Result.DateModified.ToLongDateString();
        public string Filename => Result.Filename;
        public string Path => Result.Path;
        public bool IsFolder => Result.IsFolder;
        public bool IsFile => !Result.IsFolder;

        public ICommand OpenFileInExplorerCommand { get; }

        public ResultViewModel(EverythingService.Result result, MainViewModel mainViewModel)
        {
            Result = result;
            MainViewModel = mainViewModel;

            OpenFileInExplorerCommand = new OpenFileInExplorerCommand(result.Path);
        }

        public static string FormatByteSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;

            while (bytes >= 1024 && order < sizes.Length - 1)
            {
                bytes /= 1024;
                order++;
            }

            return $"{bytes:0.##} {sizes[order]}";
        }
    }
}
