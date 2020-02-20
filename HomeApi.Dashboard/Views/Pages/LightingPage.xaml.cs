using System;
using Windows.UI.Xaml.Controls;
using HomeApi.Dashboard.Views.Components.Lighting;
using HomeApi.Dashboard.Views.Dialogs.Lighting;
using HomeApi.Dashboard.Views.Models;

namespace HomeApi.Dashboard.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LightingPage : Page
    {
        public LightingPage()
        {
            InitializeComponent();

            DataContext = new LightingPageViewModel();
        }

        private async void LightingRowItem_OnMoreInfoClicked(object sender, EventArgs e)
        {
            var rowItem = sender as LightingRowItem;
            var lightViewModel = rowItem.LightViewModel;

            var dialog = new LightingInfoDialog(lightViewModel);

            await dialog.ShowAsync();
        }
    }
}
