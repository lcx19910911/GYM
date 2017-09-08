using GYM.Core.Extensions;
using GYM.Core.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.Core.Helper
{
    public class EditorHelper
    {
        private static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "editor.json");

        public static T GetConfig<T>() where T : class
        {
            var key = CacheHelper.RenderKey(path);
            return CacheHelper.GetByFileDependency(key, path, () =>
            {
                var text = File.ReadAllText(path);
                return text.DeserializeJson<T>();
            });
        }

    }
}
