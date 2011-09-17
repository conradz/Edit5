using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Edit5.Core;
using IronJS.Hosting;

namespace Edit5.Tests
{
    [TestClass]
    public class CommandTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var context = new CSharp.Context();

            CommandObject.Attach(context);
            context.Execute("var cmd = new Command()");
            var cmd = context.Execute("cmd") as ICommand;

            Assert.IsNotNull(cmd);
        }

        [TestMethod]
        public void SetTextTest()
        {
            var context = new CSharp.Context();

            CommandObject.Attach(context);
            context.Execute(@"
                var cmd = new Command();
                cmd.setText('MyText');");
            var cmd = (ICommand)context.Execute("cmd");

            Assert.AreEqual("MyText", cmd.Text);
        }

        [TestMethod]
        public void GetTextTest()
        {
            var context = new CSharp.Context();

            CommandObject.Attach(context);
            context.Execute(@"
                var cmd = new Command();
                cmd.setText('MyText');");
            var result = context.Execute("cmd.getText()");

            Assert.AreEqual("MyText", (string)result);
        }

        [TestMethod]
        public void EventTest()
        {
            var context = new CSharp.Context();

            CommandObject.Attach(context);
            context.Execute(@"
                var cmd = new Command();
                var raised = false;
                var data = null;
                cmd.addHandler('test', function(d) {
                    raised = true;
                    data = d;
                });");
            var cmd = (ICommand)context.Execute("cmd");
            var data = "Testing";
            cmd.RaiseEvent("test", data);

            Assert.IsTrue((bool)context.Execute("raised"));
            Assert.AreEqual(data, context.Execute("data"));
        }
    }
}
