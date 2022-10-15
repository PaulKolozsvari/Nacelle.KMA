using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross;
using Nacelle.KMA.Core.Analytics;

namespace Nacelle.KMA.Core.Commands
{
    public class TrackableAsyncCommand : TrackableCommand
    {
        private readonly Func<Task> _executeAsync;
        private readonly Func<Dictionary<string, string>> _context;

        public TrackableAsyncCommand(string eventName,
            Func<Task> executeAsync,
            string target = "",
            Func<Dictionary<string, string>> context = null,
            Func<bool> canExecute = null) : base(eventName, target, canExecute)
        {
            _context = context;
            _executeAsync = executeAsync;
        }

        public override async Task ExecuteCommand(object parameter)
        {
            await _executeAsync?.Invoke();
        }

        public override void TrackEvent(object parameter)
        {
            Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(EventName, Target, _context?.Invoke());
        }
    }

    public class TrackableAsyncCommand<TParameter> : TrackableCommand where TParameter : class
    {
        private readonly Func<TParameter, Task> _executeAsync;
        private readonly Func<TParameter, Dictionary<string, string>> _context;

        public TrackableAsyncCommand(string eventName,
            Func<TParameter, Task> executeAsync,
            string target = "",
            Func<TParameter, Dictionary<string, string>> context = null,
            Func<bool> canExecute = null) : base(eventName, target, canExecute)
        {
            _context = context;
            _executeAsync = executeAsync;
        }

        public override async Task ExecuteCommand(object parameter)
        {
            await _executeAsync?.Invoke((TParameter)parameter);
        }

        public override void TrackEvent(object parameter)
        {
            Mvx.IoCProvider.Resolve<IAnalyticsService>().LogEvent(EventName, Target, _context?.Invoke((TParameter)parameter));
        }
    }
}
