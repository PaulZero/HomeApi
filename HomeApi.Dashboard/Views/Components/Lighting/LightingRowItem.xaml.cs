using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HomeApi.Dashboard.Views.Models;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace HomeApi.Dashboard.Views.Components.Lighting
{
    public sealed partial class LightingRowItem : UserControl
    {
        public event EventHandler MoreInfoClicked;

        public static readonly DependencyProperty LightViewModelProperty = DependencyProperty.Register(
            "LightViewModel", typeof(LightViewModel), typeof(LightingRowItem), new PropertyMetadata(default(LightViewModel)));

        public LightViewModel LightViewModel
        {
            get => (LightViewModel) GetValue(LightViewModelProperty);
            set => SetValue(LightViewModelProperty, value);
        }

        public LightingRowItem()
        {
            InitializeComponent();
        }

        private void MoreButton_OnClick(object sender, RoutedEventArgs e)
        {
            MoreInfoClicked?.Invoke(this, EventArgs.Empty);
        }

        private void LightingRowItem_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = LightViewModel ?? throw new Exception("Required property LightViewModel not set");
        }
    }
}
