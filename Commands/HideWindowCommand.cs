using System.Windows.Controls;
using System;
using EverythingSearch.ViewModel;
using Microsoft.Win32;

namespace EverythingSearch.Commands
{
    public class HideWindowCommand : CommandBase
    {
        public MainViewModel MainViewModel { get; set; }   

        public HideWindowCommand(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        public override void ExecuteLogic(object? parameter)
        {
            MainViewModel.HideWindows();
        }
    }
}
