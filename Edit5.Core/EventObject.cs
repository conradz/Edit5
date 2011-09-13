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
    /// <summary>
    /// Javascript object that handles events.
    /// The JS object has three methods, addHandler, removeHandler,
    /// and trigger.
    /// </summary>
    public class EventObject : CommonObject
    {
        Dictionary<string, List<FunctionObject>> events;

        public EventObject(Env env, CommonObject prototype)
            : base(env, env.Maps.Base, prototype)
        {
            events = new Dictionary<string, List<FunctionObject>>();
        }

        void Trigger(string eventName, BoxedValue data)
        {
            List<FunctionObject> handlers;
            if (events.TryGetValue(eventName, out handlers))
            {
                var globals = this.Env.Globals;
                foreach (var item in handlers)
                    item.Call(globals, data);
            }
        }

        void AddHandler(string eventName, FunctionObject handler)
        {
            List<FunctionObject> handlers;
            if (!events.TryGetValue(eventName, out handlers))
                handlers = events[eventName] = new List<FunctionObject>();

            handlers.Add(handler);
        }

        bool RemoveHandler(string eventName, FunctionObject handler)
        {
            List<FunctionObject> handlers;
            if (events.TryGetValue(eventName, out handlers))
                return handlers.Remove(handler);
            return false;
        }

        static CommonObject Construct(FunctionObject ctor, CommonObject that)
        {
            return new EventObject(ctor.Env, ctor.GetT<CommonObject>("prototype"));
        }

        static void AddHandler(
            FunctionObject func, CommonObject that,
            string eventName, FunctionObject handler)
        {
            EventObject self = that.CastTo<EventObject>();
            self.AddHandler(eventName, handler);
        }

        static void RemoveHandler(
            FunctionObject func, CommonObject that,
            string eventName, FunctionObject handler)
        {
            EventObject self = that.CastTo<EventObject>();
            self.RemoveHandler(eventName, handler);
        }

        static void Trigger(
            FunctionObject func, CommonObject that,
            string eventName, BoxedValue data)
        {
            EventObject self = that.CastTo<EventObject>();
            self.Trigger(eventName, data);
        }

        /// <summary>
        /// Attaches the EventObject type to the Javascript context.
        /// </summary>
        /// <param name="context">The context to attach to.</param>
        public static void Attach(CSharp.Context context)
        {
            var ctor = Utils.CreateConstructor(
                context.Environment, 0,
                (Func<FunctionObject, CommonObject, CommonObject>)Construct);
            var addHandler = Utils.CreateFunction(
                context.Environment, 2,
                (Action<FunctionObject, CommonObject, string, FunctionObject>)AddHandler);
            var removeHandler = Utils.CreateFunction(
                context.Environment, 2,
                (Action<FunctionObject, CommonObject, string, FunctionObject>)RemoveHandler);
            var trigger = Utils.CreateFunction(
                context.Environment, 2,
                (Action<FunctionObject, CommonObject, string, BoxedValue>)Trigger);

            var prototype = context.Environment.NewObject();
            prototype.Prototype = context.Environment.Prototypes.Object;
            prototype.Put("addHandler", addHandler);
            prototype.Put("removeHandler", removeHandler);
            prototype.Put("trigger", trigger);

            ctor.Put("prototype", prototype);

            context.SetGlobal("EventObject", ctor);
        }
    }
}
