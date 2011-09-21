using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AvalonDock;
using System.Windows.Controls;
using Edit5.Core;

namespace Edit5
{
    class Editor : IEditor
    {
        DocumentContent window;
        TextBox textBox;

        public Editor(DocumentContent window)
        {
            this.window = window;
            textBox = new TextBox();
            window.Content = textBox;
        }

        public string Title
        {
            get
            {
                return window.Title;
            }
            set
            {
                window.Title = value;
            }
        }

        public string Text
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }
    }
}
