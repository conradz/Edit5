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
    public class EditorObjectTest
    {
        EditorMock mock;
        CSharp.Context context;

        [TestInitialize]
        public void CreateContext()
        {
            context = new CSharp.Context();
            mock = new EditorMock();

            var prototype = EditorObject.CreatePrototype(context.Environment);
            context.SetGlobal("edit", new EditorObject(context.Environment, prototype, mock));
        }


        [TestMethod]
        public void GetTextTest()
        {
            var text = "My Text";
            mock.Text = text;

            string result = context.Execute<string>("edit.getText()");

            Assert.AreEqual(text, result);
        }

        [TestMethod]
        public void SetTextTest()
        {
            var text = "My Text";

            context.Execute(string.Format("edit.setText('{0}')", text));

            Assert.AreEqual(text, mock.Text);
        }

        [TestMethod]
        public void GetTitleTest()
        {
            var title = "Title";
            mock.Title = title;

            string result = context.Execute<string>("edit.getTitle()");

            Assert.AreEqual(title, result);
        }

        [TestMethod]
        public void SetTitleTest()
        {
            var title = "Title";

            context.Execute(string.Format("edit.setTitle('{0}')", title));

            Assert.AreEqual(title, mock.Title);
        }
    }
}
