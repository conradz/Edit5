using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit5.Core
{
    public interface IEditorProvider
    {
        IEditor NewEditor();
    }
}
