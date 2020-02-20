using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using HomeApi.Dashboard.Views.Models;
using HomeApi.Libraries.Models.Lighting;
using DateTime = System.DateTime;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HomeApi.Dashboard.Views.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardPage : Page
    {
        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("lighting", typeof(LightingPage)),
            ("audio", typeof(AudioPage))
        };

        public DashboardPage()
        {
            InitializeComponent();

            DataContext = new DashboardViewModel();
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void NavigationView_OnLoaded(object sender, RoutedEventArgs args)
        {
            ContentFrame.Navigated += ContentFrame_Navigated;

            NavigationView_Navigate(_pages.First().Tag);
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs args)
        {
            var item = _pages.FirstOrDefault(p => p.Page == args.SourcePageType);

            NavigationView.SelectedItem = NavigationView.MenuItems
                .OfType<NavigationViewItem>()
                .First(n => n.Tag.Equals(item.Tag));
            
            NavigationView.Header =
                ((NavigationViewItem)NavigationView.SelectedItem)?.Content?.ToString();
        }

        private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer == null) return;

            var navItemTag = args.InvokedItemContainer.Tag.ToString();

            NavigationView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
        }

        private void NavigationView_Navigate(string navItemTag, NavigationTransitionInfo transitionInfo = null)
        {
            var item = _pages.FirstOrDefault(p => p.Tag == navItemTag);
            var pageType = item.Page;

            if (pageType != null && pageType != ContentFrame.CurrentSourcePageType)
            {
                ContentFrame.Navigate(pageType, null, transitionInfo);
            }
        }

        //        private void OnBrightnessSliderChanged(object sender, RangeBaseValueChangedEventArgs e)
        //        {
        //            var slider = sender as Slider;
        //            var lightViewModel = slider?.DataContext as LightViewModel;
        //
        //            if (slider == null || lightViewModel == null) return;
        //
        //            lightViewModel.Brightness = (byte)slider.Value;
        //        }

        //        private MediaPlayer player;

        //        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        //        {
        //            if (player == null)
        //            {
        //                player = new MediaPlayer
        //                {
        //                    Source = MediaSource.CreateFromUri(new Uri("http://media-ice.musicradio.com:80/LBCUK")),
        //                    Volume = 100
        //                };
        //            }
        //
        //            player.Play();
        //        }
    }
}
