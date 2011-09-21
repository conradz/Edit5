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
    public class EditorProviderObjectTest
    {
        [TestMethod]
        public void NewEditorTest()
        {
            var mock = new EditorProviderMock();
            var context = new CSharp.Context();
            var provider = new EditorProviderObject(
                context.Environment,
                context.Environment.Prototypes.Object, mock);
            context.SetGlobal("editors", provider);

            // Create the editor
            context.Execute("var edit = editors.newEditor()");
            var edit = context.GetGlobal("edit");
            Assert.IsFalse(edit.IsUndefined);
            Assert.IsNotNull(edit.Clr);
            Assert.AreEqual(1, mock.Editors.Count);

            // Make sure it is an editor
            bool result = (bool)context.Execute("('setText' in edit)");
            Assert.IsTrue(result);
        }
    }
}
