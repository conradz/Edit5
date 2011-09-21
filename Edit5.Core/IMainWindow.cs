using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Edit5.Core
{
    public interface IMainWindow
    {
        IEditorProvider Editors { get; }

        ObservableCollection<ICommand> Commands { get; }

        ObservableCollection<ICommand> ApplicationCommands { get; }

        string Title { get; set; }

        void Exit();
    }
}
