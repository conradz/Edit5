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
    public class EventObjectTest
    {
        [TestMethod]
        public void TestEvents()
        {
            var context = new CSharp.Context();
            EventObject.Attach(context);

            bool result = (bool)context.Execute(
                @"
    var e = new EventObject();
    var triggered = false;
    e.addHandler('test', function() {
        triggered = true;
    });
    e.trigger('test');
    triggered;
");
            Assert.IsTrue(result);
        }
    }
}
