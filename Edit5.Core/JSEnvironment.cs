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
        public static CSharp.Context Main { get; set; }

        static JSEnvironment()
        {
            Main = new CSharp.Context();
        }

        public static void Initialize()
        {
            string dir = Path.GetDirectoryName(typeof(JSEnvironment).Assembly.Location);
            var files = Directory.EnumerateFiles(Path.Combine(dir, "JS"), "*.js");

            foreach (string file in files)
            {
                Main.ExecuteFile(file);
            }
        }
    }
}
