using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IronJS.Hosting;
using Edit5.Core;

namespace Edit5.Tests
{
    [TestClass]
    public class CommandsTest
    {
        [TestMethod]
        public void AddApplicationCommand()
        {
            var mock = new CommandsMock();
            var context = new CSharp.Context();
            context.SetGlobal("cmd", new CommandObject(context.Environment, context.Environment.Prototypes.Object, mock));
            context.Execute("cmd.addApplicationCommand('Value');");
            Assert.AreEqual(1, mock.ApplicationCommands.Count);
            Assert.AreEqual("Value", mock.ApplicationCommands[0]);
        }
    }
}
