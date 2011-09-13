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
    }
}
