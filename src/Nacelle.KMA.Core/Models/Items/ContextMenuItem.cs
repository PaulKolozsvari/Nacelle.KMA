using System;
using System.Threading.Tasks;

namespace Nacelle.KMA.Core.Models.Items
{
    public class ContextMenuItem
    {
        public string Text { get; set; }
        public Func<Task> Action { get; set; }
    }
}
