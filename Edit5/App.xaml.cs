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
            // Create and configure the Unity container
            container = new UnityContainer();
            container.LoadConfiguration();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Get the main window
            IMainWindow window = container.Resolve<IMainWindow>();
            this.MainWindow = (Window)window;

            // Startup
            base.OnStartup(e);

            // Startup the Javascript environment
            WindowObject.AttachToContext(JSEnvironment.Main, window);
            JSEnvironment.Initialize();

            // Show the window
            this.MainWindow.Show();
        }
    }
}
