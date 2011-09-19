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

        [TestMethod]
        public void AddChildTest()
        {
            var context = new CSharp.Context();

            CommandObject.Attach(context);
            context.Execute(@"
                var cmd = new Command();
                cmd.addChild(new Command());");
            var cmd = (ICommand)context.Execute("cmd");

            Assert.AreEqual(1, cmd.Children.Count);
        }

        [TestMethod]
        public void RemoveChildTest()
        {
            var context = new CSharp.Context();

            CommandObject.Attach(context);
            context.Execute(@"
                var cmd = new Command();
                var child = new Command();");
            context.Execute("cmd.addChild(child)");
            context.Execute("cmd.removeChild(child)");
            var cmd = (ICommand)context.Execute("cmd");

            Assert.AreEqual(0, cmd.Children.Count);
        }

        [TestMethod]
        public void SetIdTest()
        {
            var context = new CSharp.Context();
            var id = "test";

            CommandObject.Attach(context);
            context.Execute(string.Format(@"
                var cmd = new Command();
                cmd.setId('{0}');", id));

            var cmd = context.GetGlobalAs<ICommand>("cmd");
            Assert.AreEqual(id, cmd.Id);
        }

        [TestMethod]
        public void GetIdTest()
        {
            var context = new CSharp.Context();
            var id = "test";

            CommandObject.Attach(context);
            context.Execute("var cmd = new Command()");

            var cmd = context.GetGlobalAs<ICommand>("cmd");
            cmd.Id = id;

            var result = context.Execute<string>("cmd.getId()");

            Assert.AreEqual(result, id);
        }

        [TestMethod]
        public void IdChangedTest()
        {
            var context = new CSharp.Context();

            CommandObject.Attach(context);
            context.Execute("var cmd = new Command()");

            var command = context.GetGlobalAs<ICommand>("cmd");
            bool fired = false;
            command.IdChanged += delegate { fired = true; };

            context.Execute("cmd.setId('test');");

            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void SetTextTest()
        {
            var context = new CSharp.Context();
            var text = "MyText";

            CommandObject.Attach(context);
            context.Execute(string.Format(@"
                var cmd = new Command();
                cmd.setText('{0}');", text));
            var cmd = context.GetGlobalAs<ICommand>("cmd");

            Assert.AreEqual(text, cmd.Text);
        }

        [TestMethod]
        public void GetTextTest()
        {
            var context = new CSharp.Context();
            var text = "MyText";

            CommandObject.Attach(context);
            context.Execute("var cmd = new Command()");
            var cmd = context.GetGlobalAs<ICommand>("cmd");
            cmd.Text = text;

            var result = context.Execute("cmd.getText()");

            Assert.AreEqual("MyText", (string)result);
        }

        [TestMethod]
        public void TextChangedTest()
        {
            var context = new CSharp.Context();

            CommandObject.Attach(context);
            context.Execute("var cmd = new Command()");

            var command = context.GetGlobalAs<ICommand>("cmd");
            bool fired = false;
            command.TextChanged += delegate { fired = true; };

            context.Execute("cmd.setText('test')");

            Assert.IsTrue(fired);
        }
    }
}
