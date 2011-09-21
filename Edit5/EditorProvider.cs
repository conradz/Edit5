using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit5.Core;
using AvalonDock;

namespace Edit5
{
    public class EditorProvider : IEditorProvider
    {
        DockingManager manager;

        public EditorProvider(DockingManager manager)
        {
            this.manager = manager;
        }

        public IEditor NewEditor()
        {
            DocumentContent document = new DocumentContent();
            Editor editor = new Editor(document);
            document.Show(manager);
            document.Activate();
            return editor;
        }
    }
}
