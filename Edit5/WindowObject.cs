using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS;
using Env = IronJS.Environment;
using IronJS.Hosting;
using IronJS.Native;

namespace Edit5
{
    class WindowObject : CommonObject
    {
        MainWindow window;

        public WindowObject(MainWindow window, Env env, CommonObject prototype)
            : base(env, env.Maps.Base, prototype)
        {
            this.window = window;
        }

        static void SetTitle(FunctionObject func, CommonObject that, string value)
        {
            var window = that.Get("_window").Unbox<MainWindow>();
            window.Title = value;
        }

        static string GetTitle(FunctionObject func, CommonObject that)
        {
            var window = that.Get("_window").Unbox<MainWindow>();
            return window.Title;
        }

        public static void AttachToContext(CSharp.Context context, MainWindow window)
        {
            var jsWindow = context.Environment.NewObject();
            
            var setTitle = Utils.CreateFunction<Action<FunctionObject, CommonObject, string>>(context.Environment, 1, SetTitle);

            jsWindow.Put("_window", window);
            jsWindow.Put("setTitle", setTitle, DescriptorAttrs.Immutable);
            context.SetGlobal("window", jsWindow);
        }
    }
}
