using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edit5.Core
{
    public interface IEditor
    {
        string Title { get; set; }

        string Text { get; set; }
    }
}
