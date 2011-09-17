using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit5.Core
{
    public interface ICommand
    {
        string Text { get; set; }

        void RaiseEvent(string eventName, object eventData);
    }
}
