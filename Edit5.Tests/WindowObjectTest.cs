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
        public void TitleTest()
        {
            var mock = new MainWindowMock();
            var context = new CSharp.Context();
            WindowObject.AttachToContext(context, mock);

            context.Execute("window.setTitle(\"Test\")");

            Assert.AreEqual(mock.Title, "Test");
        }
    }
}
