using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Edit5.Core;

namespace Edit5
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IUnityContainer container;

        public App()
        {
            container = new UnityContainer();
            container.LoadConfiguration();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.MainWindow = (Window)container.Resolve<IMainWindow>();
            base.OnStartup(e);
            this.MainWindow.Show();
        }
    }
}
