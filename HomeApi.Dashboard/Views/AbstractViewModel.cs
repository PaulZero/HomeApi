using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using HomeApi.Dashboard.Annotations;

namespace HomeApi.Dashboard.Views
{
    public class AbstractViewModel : INotifyPropertyChanged
    {
        protected App CurrentApp => Application.Current as App;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
