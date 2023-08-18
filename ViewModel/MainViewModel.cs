using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using EverythingSearch.Commands;
using EverythingSearch.View;
using EverythingSearch.IO;
using EverythingSearch.Service;
using System.Linq;

namespace EverythingSearch.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ConfigViewModel ConfigViewModel { get; set; }
        public ObservableCollection<ResultViewModel> ResultViewModels { get; set; }

        public static ConfigWindow? ConfigWindow { get; set; }
        public static AboutWindow? AboutWindow { get; set; }

        public ICommand OpenConfigWindowCommand { get; set; }
        public ICommand CloseConfigWindowCommand { get; set; }

        public ICommand OpenAboutWindowCommand { get; set; }
        public ICommand CloseAboutWindowCommand { get; set; }

        public ICommand HideWindowCommand { get; set; }
        public ICommand QuitProgramCommand { get; set; }


        private string _searchContent;
        public string SearchContent
        {
            get
            {
                return _searchContent;
            }
            set
            {
                _searchContent = value;

                Search(value);
                OnPropertyChanged(nameof(SearchContent));
            }
        }

        private bool _isSearchResultVisible;
        public bool IsSearchResultVisible
        {
            get
            {
                return _isSearchResultVisible;
            }
            set
            {
                _isSearchResultVisible = value;
                OnPropertyChanged(nameof(IsSearchResultVisible));
            }
        }

        public MainViewModel()
        {
            ConfigViewModel                 = new(this, Saver.ProgramConfig);
            ResultViewModels                = new();

            OpenConfigWindowCommand         = new OpenConfigWindowCommand(this);
            CloseConfigWindowCommand        = new CloseConfigWindowCommand(this);

            OpenAboutWindowCommand          = new OpenAboutWindowCommand(this);
            CloseAboutWindowCommand         = new CloseAboutWindowCommand(this);

            HideWindowCommand               = new HideWindowCommand(this);
            QuitProgramCommand              = new QuitProgramCommand(this);

            _searchContent                  = "";
            _isSearchResultVisible          = false;
        }

        public void Search(string searchContent)
        {
            ClearSearchResults();
            var results = EverythingService.Search(searchContent);

            if (searchContent == null || searchContent.Trim() == "" || results.Count() == 0)
            {
                IsSearchResultVisible = false;
                return;
            }

            IsSearchResultVisible = true;
            foreach (var result in results.ToList())
            {
                ResultViewModels.Add(new(result, this));
            }
        }

        public void ClearSearchResults() => ResultViewModels.Clear();

        public void OpenAboutWindow()
        {
            CloseAboutWindow();

            AboutWindow = new();
            AboutWindow?.Show();
        }

        public void CloseAboutWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                AboutWindow?.Close();
            });
        }

        public void OpenConfigWindow()
        {
            CloseConfigWindow();

            ConfigWindow = new() { DataContext = ConfigViewModel };
            ConfigWindow.Show();
        }

        public void CloseConfigWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ConfigWindow?.Close();
            });
        }

        public void HideMainWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });
        }

        public void HideWindows()
        {
            HideMainWindow();
            CloseConfigWindow();
            CloseAboutWindow();
        }

        public void QuitProgram()
        {
            Application.Current.Shutdown();
        }
    }
}
