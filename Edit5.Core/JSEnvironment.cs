using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS;
using IronJS.Hosting;
using System.IO;

namespace Edit5.Core
{
    public static class JSEnvironment
    {
        public static CSharp.Context Main { get; private set; }
        static bool isInitialized = false;

        static JSEnvironment()
        {
            Main = new CSharp.Context();
            CommandObject.Attach(Main);
        }

        public static void Initialize()
        {
            if (isInitialized)
                return;
            else
                isInitialized = true;

            string dir = Path.GetDirectoryName(typeof(JSEnvironment).Assembly.Location);
            dir = Path.Combine(dir, "JS");

            if (Directory.Exists(dir))
            {
                var files = Directory.EnumerateFiles(dir, "*.js");

                foreach (string file in files)
                    Main.ExecuteFile(file);
            }
        }
    }
}
