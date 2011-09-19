using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit5.Core;
using System.Collections.ObjectModel;

namespace Edit5.Tests
{
    class MainWindowMock : IMainWindow
    {
        public MainWindowMock()
        {
            this.Commands = new ObservableCollection<ICommand>();
            this.ApplicationCommands = new ObservableCollection<ICommand>();
        }

        public int ExitCalled { get; set; }

        public string Title { get; set; }

        public void Exit()
        {
            ExitCalled++;
        }

        public ObservableCollection<ICommand> ApplicationCommands { get; private set; }

        public ObservableCollection<ICommand> Commands { get; private set; }
    }
}
