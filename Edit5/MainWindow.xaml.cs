using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IronJS;
using IronJS.Hosting;
using System.IO;
using Edit5.Core;
using Microsoft.Windows.Controls.Ribbon;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Edit5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindow
    {
        Commands commandManager;

        public MainWindow()
        {
            InitializeComponent();

            this.Commands = new ObservableCollection<Core.ICommand>();
            this.ApplicationCommands = new ObservableCollection<Core.ICommand>();
            commandManager = new Edit5.Commands(Ribbon);
            commandManager.BindCommands(Commands);
            commandManager.BindApplicationCommands(ApplicationCommands);
        }

        void IMainWindow.Exit()
        {
            Application.Current.Shutdown();
        }

        public ObservableCollection<Core.ICommand> Commands { get; private set; }

        public ObservableCollection<Core.ICommand> ApplicationCommands { get; private set; }

        
    }
}
