using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using HomeApi.Dashboard.Views.Models;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeApi.Dashboard.Views.Dialogs.Lighting
{
    public sealed partial class LightingInfoDialog : ContentDialog
    {
        public LightingInfoDialog(LightViewModel lightViewModel)
        {
            InitializeComponent();

            DataContext = lightViewModel;

            BrightnessSlider.Value = lightViewModel.BrightnessPercentage;
            BrightnessSlider.ValueChanged += BrightnessSlider_ValueChanged;
        }

        private void BrightnessSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            (DataContext as LightViewModel).BrightnessPercentage = (int)e.NewValue;
        }

        public LightingInfoDialog()
        {
            InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
