using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using EverythingSearch.Commands;
using EverythingSearch.Model;

namespace EverythingSearch.ViewModel
{
    public class ConfigViewModel : ViewModelBase
    {
        public MainViewModel MainViewModel { get; set; }
        public Config Config { get; set; }

        #region Middle Layer
        private string _themeColorDisplay;
        public string ThemeColorDisplay
        {
            get
            {
                return _themeColorDisplay;
            }
            set
            {
                _themeColorDisplay = value;

                if (ThemeColorDisplayDict.TryGetValue(value, out ThemeColor themeColor))
                {
                    ThemeColor = themeColor;
                }

                OnPropertyChanged(nameof(ThemeColorDisplay));
            }
        }

        private Brush _foregroundColorBrush = Brushes.Black;
        public Brush ForegroundColorBrush
        {
            get
            {
                return _foregroundColorBrush;
            }
            set
            {
                _foregroundColorBrush = value;
                OnPropertyChanged(nameof(ForegroundColorBrush));
            }
        }

        private Brush _backgroundColorBrush = Brushes.White;
        public Brush BackgroundColorBrush
        {
            get
            {
                return _backgroundColorBrush;
            }
            set
            {
                _backgroundColorBrush = value;
                OnPropertyChanged(nameof(BackgroundColorBrush));
            }
        }
        #endregion

        private double _scale;
        public double Scale 
        { 
            get 
            { 
                return _scale;
            } 
            set 
            { 
                _scale = value;
                Config.Scale = value;
                OnPropertyChanged(nameof(Scale));
            } 
        }

        private ThemeColor _themeColor;
        public ThemeColor ThemeColor
        {
            get
            {
                return _themeColor;
            }
            set
            {
                _themeColor = value;
                Config.ThemeColor = value;

                SetupColors(value);
                OnPropertyChanged(nameof(ThemeColor));
            }
        }

        private double _transparency;
        public double Transparency
        {
            get
            {
                return _transparency;
            }
            set
            {
                double limitValue = Math.Max(0.35, value);
                _transparency = limitValue;
                Config.Transparency = limitValue;

                OnPropertyChanged(nameof(Transparency));
            }
        }

        public List<string> ThemeColors => ThemeColorDisplayDict.Keys.ToList();

        public static Dictionary<string, ThemeColor> ThemeColorDisplayDict = new() {
            { "白色", ThemeColor.White }, { "黑色", ThemeColor.Black } };

        public static Dictionary<ThemeColor, Brush> BackgroundColorDict = new() {
            { ThemeColor.White, Brushes.White }, { ThemeColor.Black , Brushes.Black } };

        public static Dictionary<ThemeColor, Brush> ForegroundColorDict = new() {
            { ThemeColor.White, Brushes.Black }, { ThemeColor.Black , Brushes.White } };

        public ICommand SaveConfigCommand { get; }

        public ConfigViewModel(MainViewModel mainViewModel, Config config)
        {
            MainViewModel = mainViewModel;
            Config = config;

            _scale              = config.Scale;
            _themeColor         = config.ThemeColor;
            _transparency       = config.Transparency;

            _themeColorDisplay = ThemeColorDisplayDict.FirstOrDefault(x => x.Value == config.ThemeColor).Key ?? "Error";

            SetupColors(config.ThemeColor);
            SaveConfigCommand = new SaveConfigCommand(this);
        }

        public void SetupColors(ThemeColor themeColor)
        {
            if (ForegroundColorDict.TryGetValue(themeColor, out Brush? ForegroundColor))
            {
                ForegroundColorBrush = ForegroundColor ?? Brushes.Black;
            }

            if (BackgroundColorDict.TryGetValue(themeColor, out Brush? BackgroundColor))
            {
                BackgroundColorBrush = BackgroundColor ?? Brushes.White;
            }
        }
    }
}
