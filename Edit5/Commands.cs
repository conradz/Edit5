using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edit5.Core;
using System.Windows;
using Microsoft.Windows.Controls.Ribbon;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace Edit5
{
    class Commands
    {
        Ribbon ribbon;
        List<RibbonTabItem> tabs = new List<RibbonTabItem>();
        List<ApplicationMenuItem> appCommands = new List<ApplicationMenuItem>();

        public Commands(Ribbon ribbon)
        {
            this.ribbon = ribbon;
            ribbon.ApplicationMenu = new RibbonApplicationMenu();
        }

        public void BindCommands(ObservableCollection<ICommand> commands)
        {
            commands.CollectionChanged += CommandsChanged;
            for (int i = 0; i < commands.Count; i++)
                AddTab((ICommand)commands[i], i);
        }

        void CommandsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var items = e.NewItems;
                    for (int i = 0; i < items.Count; i++)
                        AddTab((ICommand)items[i], e.NewStartingIndex + i);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (ICommand command in e.OldItems)
                        RemoveTab(command);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        abstract class CommandItemBase
        {
            public ICommand Command { get; private set; }

            public object Item { get; protected set; }

            public CommandItemBase(ICommand command)
            {
                Command = command;
                Item = CreateItem();

                Text = command.Text;
                command.TextChanged += command_TextChanged;
            }

            void command_TextChanged(object sender, EventArgs e)
            {
                Text = Command.Text;
            }

            public virtual void Remove()
            {
                Command.TextChanged -= command_TextChanged;
            }

            protected abstract object CreateItem();

            public abstract string Text { get; set; }
        }

        abstract class CommandItem<TChild> : CommandItemBase
            where TChild : CommandItemBase
        {
            public List<TChild> Children { get; private set; }

            protected CommandItem(ICommand command)
                : base(command)
            {
                Children = new List<TChild>();

                for (int i = 0, length = command.Children.Count; i < length; i++)
                    AddChild(i, command.Children[i]);
                
                command.Children.CollectionChanged += ChildrenChanged;
            }

            void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        for (int i = 0; i < e.NewItems.Count; i++)
                            AddChild(i + e.NewStartingIndex, (ICommand)e.NewItems[i]);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (var item in e.OldItems)
                            RemoveChild((ICommand)item);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            public override void Remove()
            {
                foreach (var item in Children)
                    RemoveItem(item);
                Command.Children.CollectionChanged -= ChildrenChanged;
            }

            protected abstract void AddChild(int index, ICommand command);

            protected virtual void RemoveChild(ICommand command)
            {
                foreach (var item in Children)
                {
                    if (item.Command == command)
                    {
                        RemoveItem(item);
                        break;
                    }
                }
            }

            protected virtual void RemoveItem(TChild item)
            {
                Children.Remove(item);
                item.Remove();
            }
        }

        #region Tab

        void AddTab(ICommand command, int index)
        {
            var tab = new RibbonTabItem(command);
            ribbon.Items.Insert(index, tab.Item);
            tabs.Insert(index, tab);
        }

        void RemoveTab(ICommand command)
        {
            foreach (var tab in tabs)
            {
                if (tab.Command == command)
                {
                    tab.Remove();
                    ribbon.Items.Remove(tab.Item);
                    tabs.Remove(tab);
                    break;
                }
            }
        }

        class RibbonTabItem : CommandItem<RibbonGroupItem>
        {
            public RibbonTabItem(ICommand command)
                : base(command)
            {
            }


            public override string Text
            {
                get
                {
                    return (string)((RibbonTab)Item).Header;
                }
                set
                {
                    ((RibbonTab)Item).Header = value;
                }
            }

            protected override void AddChild(int index, ICommand command)
            {
                var item = new RibbonGroupItem(command);
                Children.Insert(index, item);
                ((RibbonTab)Item).Items.Insert(index, item.Item);
            }

            protected override void RemoveItem(RibbonGroupItem item)
            {
                base.RemoveItem(item);
                ((RibbonTab)Item).Items.Remove(item);
            }

            protected override object CreateItem()
            {
                return new RibbonTab();
            }
        }

        #endregion

        #region Group

        class RibbonGroupItem : CommandItem<RibbonButtonItem>
        {
            public RibbonGroupItem(ICommand command)
                : base(command)
            {
            }

            public override string Text
            {
                get
                {
                    return (string)((RibbonGroup)Item).Header;
                }
                set
                {
                    ((RibbonGroup)Item).Header = value;
                }
            }

            protected override void AddChild(int index, ICommand command)
            {
                var item = new RibbonButtonItem(command);
                Children.Insert(index, item);
                ((RibbonGroup)Item).Items.Insert(index, item.Item);
            }

            protected override void RemoveItem(RibbonButtonItem item)
            {
                base.RemoveItem(item);
                ((RibbonGroup)Item).Items.Remove(item.Item);
            }

            protected override object CreateItem()
            {
                return new RibbonGroup();
            }
        }

        #endregion

        #region Button

        class RibbonButtonItem : CommandItemBase
        {
            public RibbonButtonItem(ICommand command)
                : base(command)
            {
                ((RibbonButton)Item).Click += item_Click;
            }

            void item_Click(object sender, RoutedEventArgs e)
            {
                e.Handled = true;
                Command.RaiseEvent("click", null);
            }

            protected override object CreateItem()
            {
                return new RibbonButton();
            }

            public override string Text
            {
                get
                {
                    return ((RibbonButton)Item).Label;
                }
                set
                {
                    ((RibbonButton)Item).Label = value;
                }
            }

            public override void Remove()
            {
                base.Remove();
                ((RibbonButton)Item).Click -= item_Click;
            }
        }

        #endregion

        public void BindApplicationCommands(ObservableCollection<ICommand> commands)
        {
            for (int i = 0; i < commands.Count; i++)
                AddApplicationCommand(i, commands[i]);
            commands.CollectionChanged += new NotifyCollectionChangedEventHandler(commands_CollectionChanged);
        }

        void commands_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    for (int i = 0, length = e.NewItems.Count; i < length; i++)
                        AddApplicationCommand(i + e.NewStartingIndex, (ICommand)e.NewItems[i]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (ICommand command in e.OldItems)
                        RemoveApplicationCommand(command);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        void AddApplicationCommand(int index, ICommand command)
        {
            var item = new ApplicationMenuItem(command);
            appCommands.Insert(index, item);
            ribbon.ApplicationMenu.Items.Insert(index, item.Item);
        }

        void RemoveApplicationCommand(ICommand command)
        {
            foreach (var item in appCommands)
            {
                if (item.Command == command)
                {
                    appCommands.Remove(item);
                    ribbon.ApplicationMenu.Items.Remove(item.Item);
                    break;
                }
            }
        }

        class ApplicationMenuItem : CommandItem<ApplicationMenuItem>
        {
            public ApplicationMenuItem(ICommand command)
                : base(command)
            {
                RibbonApplicationMenuItem item = (RibbonApplicationMenuItem)Item;
                item.Click += item_Click;
            }

            void item_Click(object sender, RoutedEventArgs e)
            {
                e.Handled = true;
                Command.RaiseEvent("click", null);
            }

            protected override void AddChild(int index, ICommand command)
            {
                var item = new ApplicationMenuItem(command);
                Children.Insert(index, item);
                ((RibbonApplicationMenuItem)Item).Items.Insert(index, item.Item);
            }

            protected override void RemoveItem(ApplicationMenuItem item)
            {
                base.RemoveItem(item);
                ((RibbonApplicationMenuItem)Item).Items.Remove(item.Item);
            }

            public override void Remove()
            {
                base.Remove();
                ((RibbonApplicationMenuItem)Item).Click -= item_Click;
            }

            protected override object CreateItem()
            {
                return new RibbonApplicationMenuItem();
            }

            public override string Text
            {
                get
                {
                    return (string)((RibbonApplicationMenuItem)Item).Header;
                }
                set
                {
                    ((RibbonApplicationMenuItem)Item).Header = value;
                }
            }
        }
    }
}
