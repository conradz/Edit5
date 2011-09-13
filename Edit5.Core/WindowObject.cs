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
    public static class WindowObject
    {
        static void SetTitle(FunctionObject func, CommonObject that, string value)
        {
            var window = that.Get("_window").Unbox<IMainWindow>();
            window.Title = value;
        }

        static string GetTitle(FunctionObject func, CommonObject that)
        {
            var window = that.Get("_window").Unbox<IMainWindow>();
            return window.Title;
        }

        public static void AttachToContext(CSharp.Context context, IMainWindow window)
        {
            var jsWindow = context.Environment.NewObject();
            
            var setTitle = Utils.CreateFunction<Action<FunctionObject, CommonObject, string>>(context.Environment, 1, SetTitle);

            jsWindow.Put("_window", window);
            jsWindow.Put("setTitle", setTitle, DescriptorAttrs.Immutable);
            context.SetGlobal("window", jsWindow);
        }
    }
}
