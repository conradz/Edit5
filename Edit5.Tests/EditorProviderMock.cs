using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit5.Core;

namespace Edit5.Tests
{
    class EditorProviderMock : IEditorProvider
    {
        public List<EditorMock> Editors { get; private set; }

        public EditorProviderMock()
        {
            Editors = new List<EditorMock>();
        }

        public IEditor NewEditor()
        {
            var editor = new EditorMock();
            Editors.Add(editor);
            return editor;
        }
    }
}
