using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit5.Core;
using System.Windows;
using Microsoft.Windows.Controls.Ribbon;

namespace Edit5
{
    class Commands : ICommands
    {
        Ribbon ribbon;

        public Commands(MainWindow window)
        {
            ribbon = window.Ribbon;
            ribbon.ApplicationMenu = new RibbonApplicationMenu();
            ribbon.QuickAccessToolBar = new RibbonQuickAccessToolBar();
        }

        public void AddApplicationCommand(ICommand command)
        {
            var item = new RibbonApplicationMenuItem();
            item.Header = command.Text;
            item.Click += delegate
            {
                command.RaiseEvent("click", null);
            };

            ribbon.ApplicationMenu.Items.Add(item);
        }
    }
}
