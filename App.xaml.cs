using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using EverythingSearch.ViewModel;
using EverythingSearch.IO;

namespace EverythingSearch
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Saver.LoadConfig();
            Saver.StartService();

            MainWindow = new MainWindow() { DataContext = new MainViewModel() };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void HandleException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"额, 貌似出了点问题...\n{e.Exception.Message}\n{e.Exception.StackTrace}", "警告", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
