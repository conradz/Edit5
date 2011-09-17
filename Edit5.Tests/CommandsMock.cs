using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit5.Core;

namespace Edit5.Tests
{
    class CommandsMock : ICommands
    {
        public List<ICommand> ApplicationCommands { get; private set; }

        public CommandsMock()
        {
            ApplicationCommands = new List<ICommand>();
        }

        public void AddApplicationCommand(ICommand command)
        {
            ApplicationCommands.Add(command);
        }
    }
}
