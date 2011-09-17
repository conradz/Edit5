using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS;
using Env = IronJS.Environment;
using IronJS.Native;

namespace Edit5.Core
{
    public class CommandObject : CommonObject
    {
        ICommands commands;

        public CommandObject(Env env, CommonObject prototype, ICommands commands)
            : base(env, env.Maps.Base, prototype)
        {
            this.commands = commands;
            this.Put("addApplicationCommand",
                Utils.CreateFunction(
                    env, 1,
                    (Action<FunctionObject, CommonObject, string>)AddApplicationCommand));
        }

        static void AddApplicationCommand(FunctionObject func, CommonObject that, string text)
        {
            var self = that.CastTo<CommandObject>();
            self.commands.AddApplicationCommand(text);
        }
    }
}
