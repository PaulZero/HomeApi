using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using HomeApi.Dashboard.Libraries.DI;
using HomeApi.Dashboard.Views;

namespace HomeApi.Dashboard.Services.UserInteraction
{
    public class IdleService : IService
    {
        public event EventHandler IdleChanged;

        public bool IsIdle
        {
            get => _isIdle;
            private set
            {
                if (_isIdle == value) return;

                _isIdle = value;

                IdleChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private bool _isIdle;
        private readonly TimeSpan _timeoutDuration = TimeSpan.FromSeconds(30);
        private readonly DispatcherTimer _timer;

        public IdleService()
        {
            _timer = new DispatcherTimer {Interval = _timeoutDuration};

            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, object e)
        {
            _timer.Stop();

            IsIdle = true;
        }

        public async Task InitialiseAsync()
        {
            Window.Current.CoreWindow.PointerMoved += CoreWindow_PointerMoved;

            _timer.Start();
        }

        private void CoreWindow_PointerMoved(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.PointerEventArgs args)
        {
            _timer.Stop();
            _timer.Start();

            IsIdle = false;
        }
    }
}
