using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Edit5.Core;
using IronJS.Hosting;

namespace Edit5.Tests
{
    /// <summary>
    /// Contains all tests for WindowObject.
    /// </summary>
    [TestClass]
    public class WindowObjectTest
    {
        [TestMethod]
        public void SetTitleTest()
        {
            var mock = new MainWindowMock();
            var context = new CSharp.Context();
            WindowObject.AttachToContext(context, mock);

            context.Execute("window.setTitle(\"Test\")");

            Assert.AreEqual(mock.Title, "Test");
        }

        [TestMethod]
        public void GetTitleTest()
        {
            var mock = new MainWindowMock();
            var context = new CSharp.Context();
            WindowObject.AttachToContext(context, mock);

            string title = "Test";
            mock.Title = title;
            string result = context.Execute("window.getTitle()").ToString();

            Assert.AreEqual(result, title);
        }

        [TestMethod]
        public void ExitTest()
        {
            var mock = new MainWindowMock();
            var context = new CSharp.Context();
            WindowObject.AttachToContext(context, mock);

            context.Execute("window.exit()");

            Assert.AreEqual(mock.ExitCalled, 1);
        }

        [TestMethod]
        public void AddCommandTest()
        {
            var mock = new MainWindowMock();
            var context = new CSharp.Context();
            WindowObject.AttachToContext(context, mock);
            CommandObject.Attach(context);

            context.Execute("window.addCommand(new Command())");

            Assert.AreEqual(mock.Commands.Count, 1);
        }

        [TestMethod]
        public void RemoveCommandTest()
        {
            var mock = new MainWindowMock();
            var context = new CSharp.Context();
            WindowObject.AttachToContext(context, mock);
            CommandObject.Attach(context);

            context.Execute(@"
                var c = new Command();
                window.addCommand(c);
                window.removeCommand(c);");

            Assert.AreEqual(mock.Commands.Count, 0);
        }

        [TestMethod]
        public void AddApplicationCommandTest()
        {
            var mock = new MainWindowMock();
            var context = new CSharp.Context();
            WindowObject.AttachToContext(context, mock);
            CommandObject.Attach(context);

            context.Execute("window.addApplicationCommand(new Command())");

            Assert.AreEqual(mock.ApplicationCommands.Count, 1);
        }

        [TestMethod]
        public void RemoveApplicationCommandTest()
        {
            var mock = new MainWindowMock();
            var context = new CSharp.Context();
            WindowObject.AttachToContext(context, mock);
            CommandObject.Attach(context);

            context.Execute(@"
                var c = new Command();
                window.addApplicationCommand(c);
                window.removeApplicationCommand(c);");

            Assert.AreEqual(mock.ApplicationCommands.Count, 0);
        }
    }
}
