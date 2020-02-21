using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using HomeApi.Dashboard.Views.Models;
using HomeApi.Libraries.Models.Lighting;

namespace HomeApi.Dashboard.Views.Components.Lighting
{
    public sealed partial class BrightnessRange : UserControl
    {
        public static readonly DependencyProperty BrightnessPercentagesProperty = DependencyProperty.Register(
            "BrightnessPercentages", typeof(int[]), typeof(BrightnessRange), new PropertyMetadata(new[] {0, 25, 50, 75, 100}));

        public static readonly DependencyProperty LightProperty = DependencyProperty.Register(
            "Light", typeof(Light), typeof(BrightnessRange), new PropertyMetadata(default(Light)));

        public int[] BrightnessPercentages
        {
            get => (int[]) GetValue(BrightnessPercentagesProperty);
            set => SetValue(BrightnessPercentagesProperty, value);
        }

        public Light Light
        {
            get => (Light) GetValue(LightProperty);
            set => SetValue(LightProperty, value);
        }

        public ObservableCollection<BrightnessRangeItem> RangeItems { get; }
            = new ObservableCollection<BrightnessRangeItem>();

        public BrightnessRange()
        {
            InitializeComponent();

            Loaded += BrightnessRange_Loaded;
        }

        private void BrightnessRange_Loaded(object sender, RoutedEventArgs e)
        {
            BrightnessPercentages
                .Where(p => p >= 0 && p <= 100)
                .Distinct()
                .OrderBy(p => p)
                .ToList()
                .ForEach(p => RangeItems.Add(new BrightnessRangeItem(p, this)));

            RangeItemsControl.ItemsSource = RangeItems;
        }

        public class BrightnessRangeItem : AbstractViewModel
        {
            public bool IsPercentageMatch => _parentControl.Light.BrightnessPercentage == Percentage;

            public string Label => $"{Percentage}%";

            public string GroupName => _parentControl.Light.Name;

            public int Percentage { get; }

            public bool IsChecked
            {
                get => _isChecked;
                set
                {
                    if (_isChecked == value) return;
                    
                    _isChecked = value;
                    
                    NotifyPropertyChanged();
                } 
            }

            public bool IsEnabled
            {
                get => _isEnabled;
                set
                {
                    if (_isEnabled == value) return;

                    _isEnabled = value; 

                    NotifyPropertyChanged();
                }
            }

            private bool _isEnabled;
            private bool _isChecked;
            private readonly BrightnessRange _parentControl;

            public BrightnessRangeItem(int percentage, BrightnessRange parentControl)
            {
                _parentControl = parentControl;

                Percentage = percentage;
                IsEnabled = _parentControl.Light.IsOn;
                IsChecked = IsPercentageMatch;

                _parentControl.Light.PropertyChanged += Light_PropertyChanged;
            }

            private void Light_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(LightViewModel.Brightness))
                {
                    IsChecked = IsPercentageMatch;
                }

                if (e.PropertyName == nameof(LightViewModel.IsOn))
                {
                    IsEnabled = _parentControl.Light.IsOn;
                }
            }
        }

        private void Brightness_Select(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            var percentage = int.Parse(radioButton.Tag.ToString());

            Light.BrightnessPercentage = percentage;
        }
    }
}