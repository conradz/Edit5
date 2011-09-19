using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS;
using Env = IronJS.Environment;
using IronJS.Hosting;
using IronJS.Native;
using System.Collections.ObjectModel;

namespace Edit5.Core
{
    public class CommandObject : EventObject, ICommand
    {
        #region Implementation

        private string text, id;

        public CommandObject(Env env, CommonObject prototype)
            : base(env, prototype)
        {
            Children = new ObservableCollection<ICommand>();
        }

        public string Text
        {
            get { return text; }
            set
            {
                if (value != text)
                {
                    text = value;
                    OnTextChanged();
                }
            }
        }

        public string Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnIdChanged();
                }
            }
        }

        public event EventHandler IdChanged;

        public event EventHandler TextChanged;

        public ObservableCollection<ICommand> Children { get; private set; }

        protected virtual void OnIdChanged()
        {
            if (IdChanged != null)
                IdChanged.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnTextChanged()
        {
            if (TextChanged != null)
                TextChanged.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region JS Interop

        static void SetText(FunctionObject func, CommonObject that, string text)
        {
            var self = that.CastTo<CommandObject>();
            self.Text = text;
        }

        static string GetText(FunctionObject func, CommonObject that)
        {
            var self = that.CastTo<CommandObject>();
            return self.Text;
        }

        static void SetID(FunctionObject func, CommonObject that, string id)
        {
            var self = that.CastTo<CommandObject>();
            self.Id = id;
        }

        static string GetID(FunctionObject func, CommonObject that)
        {
            var self = that.CastTo<CommandObject>();
            return self.Id;
        }

        static ArrayObject GetChildren(FunctionObject func, CommonObject that)
        {
            var children = that.CastTo<CommandObject>().Children;
            var array = new ArrayObject(func.Env, (uint)children.Count);
            for (int i = 0; i < children.Count; i++)
                array.Put(i, BoxingUtils.JsBox(children[i]));
            return array;
        }

        static void AddChild(FunctionObject func, CommonObject that, CommonObject command)
        {
            that.CastTo<CommandObject>().Children.Add(command.CastTo<CommandObject>());
        }

        static void RemoveChild(FunctionObject func, CommonObject that, CommonObject command)
        {
            that.CastTo<CommandObject>().Children.Remove(command.CastTo<CommandObject>());
        }

        static CommonObject Construct(FunctionObject func, CommonObject that)
        {
            return new CommandObject(func.Env, func.GetT<CommonObject>("prototype"));
        }

        public new static void Attach(CSharp.Context context)
        {
            context.SetGlobal("Command", Create(context));
        }

        public new static FunctionObject Create(CSharp.Context context)
        {
            var ctor = Utils.CreateConstructor(
                context.Environment, 0,
                (Func<FunctionObject, CommonObject, CommonObject>)Construct);
            ctor.Put("prototype", CreatePrototype(context));

            return ctor;
        }

        public new static CommonObject CreatePrototype(CSharp.Context context)
        {
            var setText = Utils.CreateFunction(
                context.Environment, 1,
                (Action<FunctionObject, CommonObject, string>)SetText);
            var getText = Utils.CreateFunction(
                context.Environment, 0,
                (Func<FunctionObject, CommonObject, string>)GetText);
            var setId = Utils.CreateFunction(
                context.Environment, 1,
                (Action<FunctionObject, CommonObject, string>)SetID);
            var getId = Utils.CreateFunction(
                context.Environment, 0,
                (Func<FunctionObject, CommonObject, string>)GetID);
            var getChildren = Utils.CreateFunction(
                context.Environment, 0,
                (Func<FunctionObject, CommonObject, ArrayObject>)GetChildren);
            var addChild = Utils.CreateFunction(
                context.Environment, 1,
                (Action<FunctionObject, CommonObject, CommonObject>)AddChild);
            var removeChild = Utils.CreateFunction(
                context.Environment, 1,
                (Action<FunctionObject, CommonObject, CommonObject>)RemoveChild);

            var prototype = EventObject.CreatePrototype(context);

            prototype.Put("setText", setText, DescriptorAttrs.Immutable);
            prototype.Put("getText", getText, DescriptorAttrs.Immutable);
            prototype.Put("getId", getId, DescriptorAttrs.Immutable);
            prototype.Put("setId", setId, DescriptorAttrs.Immutable);
            prototype.Put("getChildren", getChildren, DescriptorAttrs.Immutable);
            prototype.Put("addChild", addChild, DescriptorAttrs.Immutable);
            prototype.Put("removeChild", removeChild, DescriptorAttrs.Immutable);

            return prototype;
        }

        #endregion
    }
}
