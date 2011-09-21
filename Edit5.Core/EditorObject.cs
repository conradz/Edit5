using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS;
using Env = IronJS.Environment;
using IronJS.Native;

namespace Edit5.Core
{
    public class EditorObject : CommonObject
    {
        IEditor editor;

        delegate void SetStr(FunctionObject func, CommonObject that, string value);
        delegate string GetStr(FunctionObject func, CommonObject that);

        public EditorObject(Env env, CommonObject prototype, IEditor editor)
            : base(env, env.Maps.Base, prototype)
        {
            this.editor = editor;
        }

        static string GetText(FunctionObject func, CommonObject that)
        {
            var self = that.CastTo<EditorObject>();
            return self.editor.Text;
        }

        static void SetText(FunctionObject func, CommonObject that, string text)
        {
            var self = that.CastTo<EditorObject>();
            self.editor.Text = text;
        }

        static string GetTitle(FunctionObject func, CommonObject that)
        {
            var self = that.CastTo<EditorObject>();
            return self.editor.Title;
        }

        static void SetTitle(FunctionObject func, CommonObject that, string text)
        {
            var self = that.CastTo<EditorObject>();
            self.editor.Title = text;
        }

        public static CommonObject CreatePrototype(Env env)
        {
            var getText = Utils.CreateFunction<GetStr>(env, 0, GetText);
            var setText = Utils.CreateFunction<SetStr>(env, 1, SetText);
            var getTitle = Utils.CreateFunction<GetStr>(env, 0, GetTitle);
            var setTitle = Utils.CreateFunction<SetStr>(env, 1, SetTitle);

            var prototype = new CommonObject(env, env.Maps.Base, env.Prototypes.Object);

            prototype.Put("getText", getText, DescriptorAttrs.Immutable);
            prototype.Put("setText", setText, DescriptorAttrs.Immutable);
            prototype.Put("getTitle", getTitle, DescriptorAttrs.Immutable);
            prototype.Put("setTitle", setTitle, DescriptorAttrs.Immutable);

            return prototype;
        }
    }
}
