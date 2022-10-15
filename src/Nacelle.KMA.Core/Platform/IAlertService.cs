using System;
using System.Threading.Tasks;

namespace Nacelle.KMA.Core.Platform
{
    public interface IAlertService
    {
        Task Show(string title, string message, (string Title, Func<Task> Action) acceptAction, (string Title, Func<Task> Action)? cancelAction = null);
    }
}
