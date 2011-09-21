using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit5.Core;

namespace Edit5.Tests
{
    class EditorMock : IEditor
    {
        public string Title { get; set; }

        public string Text { get; set; }
    }
}
