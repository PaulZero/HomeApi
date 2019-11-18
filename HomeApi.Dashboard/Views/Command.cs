using System;
using System.ComponentModel;
using System.Windows.Input;

namespace HomeApi.Dashboard.Views
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<object> _executeCallback;

        private readonly Func<object, bool> _canExecuteCallback;

        public Command(Action<object> executeCallback, Func<object, bool> canExecuteCallback)
        {
            _executeCallback = executeCallback;
            _canExecuteCallback = canExecuteCallback;
        }

        public void WatchProperty(INotifyPropertyChanged model, string propertyName)
        {
            model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteCallback(parameter);
        }

        public void Execute(object parameter)
        {
            _executeCallback(parameter);
        }
    }
}