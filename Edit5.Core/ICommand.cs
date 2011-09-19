using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace Edit5.Core
{
    public interface ICommand
    {
        /// <summary>
        /// Fired when the <c>Id</c> property changes.
        /// </summary>
        event EventHandler IdChanged;

        /// <summary>
        /// Fired when the <c>Text</c> property changes.
        /// </summary>
        event EventHandler TextChanged;

        /// <summary>
        /// Gets or sets the ID of the command.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the text of the command that is displayed in the UI.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Fires an event on the command. Javascript code can add
        /// handlers for these events.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="eventData">The data to pass to all even handlers.</param>
        void RaiseEvent(string eventName, object eventData);

        /// <summary>
        /// Gets the child commands.
        /// </summary>
        ObservableCollection<ICommand> Children { get; }
    }
}
