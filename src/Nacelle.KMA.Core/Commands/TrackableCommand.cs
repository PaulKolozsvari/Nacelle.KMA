using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nacelle.KMA.Core.Commands
{
    public abstract class TrackableCommand : ICommand
    {
        public string EventName { get; }
        public string Target { get; }
        public Func<bool> CanExecuteCommand { get; }

        public TrackableCommand(string eventName,
            string target = "",
            Func<bool> canExecute = null)
        {
            CanExecuteCommand = canExecute;
            Target = target;
            EventName = eventName;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            CanExecuteCommand?.Invoke();
            return CanExecuteCommand == null;
        }

        public void Execute(object parameter)
        {
            TrackEvent(parameter);
            ExecuteCommand(parameter);
        }

        public abstract Task ExecuteCommand(object parameter);
        public abstract void TrackEvent(object parameter);
    }
}
