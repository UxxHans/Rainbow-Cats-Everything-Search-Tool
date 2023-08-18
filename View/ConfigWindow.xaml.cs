using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EverythingSearch.IO;

namespace EverythingSearch.View
{
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();
            Loaded += ConfigWindowLoaded;
            Closed += ConfigWindowClosed;
        }

        private void ConfigWindowClosed(object? sender, EventArgs e)
        {
            Saver.SaveConfig();
        }

        private void ConfigWindowLoaded(object sender, RoutedEventArgs e)
        {
            GetWindow(this).KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                    break;
                case Key.D1:
                    break;
            }
        }
    }
}
