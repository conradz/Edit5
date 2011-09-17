using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS;
using Env = IronJS.Environment;
using IronJS.Hosting;
using IronJS.Native;

namespace Edit5.Core
{
    public class CommandObject : EventObject, ICommand
    {
        public CommandObject(Env env, CommonObject prototype)
            : base(env, prototype)
        {
        }

        public string Text { get; set; }

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

        static CommonObject Construct(FunctionObject func, CommonObject that)
        {
            return new CommandObject(func.Env, func.GetT<CommonObject>("prototype"));
        }

        public static void Attach(CSharp.Context context)
        {
            context.SetGlobal("Command", Create(context));
        }

        public static FunctionObject Create(CSharp.Context context)
        {
            var ctor = Utils.CreateConstructor(
                context.Environment, 0,
                (Func<FunctionObject, CommonObject, CommonObject>)Construct);
            ctor.Put("prototype", CreatePrototype(context));

            return ctor;
        }

        public static CommonObject CreatePrototype(CSharp.Context context)
        {
            var setText = Utils.CreateFunction(
                context.Environment, 1,
                (Action<FunctionObject, CommonObject, string>)SetText);
            var getText = Utils.CreateFunction(
                context.Environment, 0,
                (Func<FunctionObject, CommonObject, string>)GetText);

            var prototype = EventObject.CreatePrototype(context);

            prototype.Put("setText", setText);
            prototype.Put("getText", getText);

            return prototype;
        }
    }
}
