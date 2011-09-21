using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS;
using Env = IronJS.Environment;
using IronJS.Native;

namespace Edit5.Core
{
    public class EditorProviderObject : CommonObject
    {
        IEditorProvider provider;
        CommonObject editorPrototype;

        public EditorProviderObject(Env env, CommonObject prototype, IEditorProvider provider)
            : base(env, env.Maps.Base, prototype)
        {
            this.provider = provider;
            this.editorPrototype = EditorObject.CreatePrototype(env);

            var newEditor = Utils.CreateFunction(env, 0,
                (Func<FunctionObject, CommonObject, CommonObject>)NewEditor);
            this.Put("newEditor", newEditor, DescriptorAttrs.Immutable);
        }

        static CommonObject NewEditor(FunctionObject func, CommonObject that)
        {
            var self = that.CastTo<EditorProviderObject>();
            IEditor editor = self.provider.NewEditor();
            return new EditorObject(func.Env, self.editorPrototype, editor);
        }
    }
}
