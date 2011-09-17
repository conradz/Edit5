﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit5.Core;

namespace Edit5.Tests
{
    class MainWindowMock : IMainWindow
    {
        public MainWindowMock()
        {
            this.Commands = new CommandsMock();
        }

        public int ExitCalled { get; set; }

        public string Title { get; set; }

        public void Exit()
        {
            ExitCalled++;
        }

        public ICommands Commands { get; private set; }
    }
}
