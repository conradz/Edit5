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
    public class WindowObject : CommonObject
    {
        IMainWindow window;

        public WindowObject(Env env, CommonObject prototype, IMainWindow window)
            : base(env, env.Maps.Base, prototype)
        {
            this.window = window;
        }

        static void SetTitle(FunctionObject func, CommonObject that, string value)
        {
            var window = that.CastTo<WindowObject>().window;
            window.Title = value;
        }

        static string GetTitle(FunctionObject func, CommonObject that)
        {
            var window = that.CastTo<WindowObject>().window;
            return window.Title;
        }

        static void AddCommand(FunctionObject func, CommonObject that, CommonObject command)
        {
            var window = that.CastTo<WindowObject>().window;
            window.Commands.Add((ICommand)command);
        }

        static void RemoveCommand(FunctionObject func, CommonObject that, CommonObject command)
        {
            var window = that.CastTo<WindowObject>().window;
            window.Commands.Remove((ICommand)command);
        }

        static void AddApplicationCommand(FunctionObject func, CommonObject that, CommonObject command)
        {
            var window = that.CastTo<WindowObject>().window;
            window.ApplicationCommands.Add((ICommand)command);
        }

        static void RemoveApplicationCommand(FunctionObject func, CommonObject that, CommonObject command)
        {
            var window = that.CastTo<WindowObject>().window;
            window.ApplicationCommands.Remove((ICommand)command);
        }

        static void Exit(FunctionObject func, CommonObject that)
        {
            var window = that.CastTo<WindowObject>().window;
            window.Exit();
        }

        public static void AttachToContext(CSharp.Context context, IMainWindow window)
        {
            var jsWindow = new WindowObject(
                context.Environment,
                context.Environment.Prototypes.Object,
                window);
            
            var setTitle = Utils.CreateFunction(
                context.Environment, 1,
                (Action<FunctionObject, CommonObject, string>)SetTitle);
            var getTitle = Utils.CreateFunction(
                context.Environment, 0,
                (Func<FunctionObject, CommonObject, string>)GetTitle);
            var addCommand = Utils.CreateFunction(
                context.Environment, 1,
                (Action<FunctionObject, CommonObject, CommonObject>)AddCommand);
            var removeCommand = Utils.CreateFunction(
                context.Environment, 1,
                (Action<FunctionObject, CommonObject, CommonObject>)RemoveCommand);
            var addApplicationCommand = Utils.CreateFunction(
                context.Environment, 1,
                (Action<FunctionObject, CommonObject, CommonObject>)AddApplicationCommand);
            var removeApplicationCommand = Utils.CreateFunction(
                context.Environment, 1,
                (Action<FunctionObject, CommonObject, CommonObject>)RemoveApplicationCommand);
            var exit = Utils.CreateFunction(
                context.Environment, 0,
                (Action<FunctionObject, CommonObject>)Exit);

            jsWindow.Put("setTitle", setTitle, DescriptorAttrs.Immutable);
            jsWindow.Put("getTitle", getTitle, DescriptorAttrs.Immutable);
            jsWindow.Put("addCommand", addCommand, DescriptorAttrs.Immutable);
            jsWindow.Put("removeCommand", removeCommand, DescriptorAttrs.Immutable);
            jsWindow.Put("addApplicationCommand", addApplicationCommand, DescriptorAttrs.Immutable);
            jsWindow.Put("removeApplicationCommand", removeApplicationCommand, DescriptorAttrs.Immutable);
            jsWindow.Put("exit", exit, DescriptorAttrs.Immutable);

            context.SetGlobal("window", jsWindow);
        }
    }
}
