using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit5.Core;

namespace Edit5.Tests
{
    class CommandsMock : ICommands
    {
        public List<string> ApplicationCommands { get; private set; }

        public CommandsMock()
        {
            ApplicationCommands = new List<string>();
        }

        public void AddApplicationCommand(string text)
        {
            ApplicationCommands.Add(text);
        }
    }
}
