using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS;
using Env = IronJS.Environment;
using IronJS.Native;

namespace Edit5.Core
{
    public class CommandsObject : CommonObject
    {
        ICommands commands;

        public CommandsObject(Env env, CommonObject prototype, ICommands commands)
            : base(env, env.Maps.Base, prototype)
        {
            this.commands = commands;
            this.Put("addApplicationCommand",
                Utils.CreateFunction(
                    env, 1,
                    (Action<FunctionObject, CommonObject, CommonObject>)AddApplicationCommand));
        }

        static void AddApplicationCommand(FunctionObject func, CommonObject that, CommonObject command)
        {
            var self = that.CastTo<CommandsObject>();
            self.commands.AddApplicationCommand(command.CastTo<CommandObject>());
        }
    }
}
