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

        public void AddApplicationCommand(string text)
        {
            ribbon.ApplicationMenu.Items.Add(new RibbonApplicationMenuItem() { Header = text });
        }
    }
}
