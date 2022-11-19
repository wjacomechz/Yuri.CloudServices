using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YURI.CLOUD.TRANSVERSAL.COMMON
{
    public static class Extensions
    {
        public static string GetCaller(this object obj, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            return obj.GetType().Name.Split('`')[0] + "." + memberName;
        }

        public static string GetCaller(this Type obj, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            return obj.Name.Split('`')[0] + "." + memberName;
        }

        public static object GetNestedPropertyValue(this object obj, string name)
        {
            foreach (string part in name.Split('.'))
            {
                if (obj == null) return null;
                var type = obj.GetType();
                var info = type.GetProperty(part);
                if (info == null) return null;
                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static void SetNestedPropertyValue(this object obj, string name, object value)
        {
            foreach (string part in name.Split('.'))
            {
                if (obj == null) return;
                var type = obj.GetType();
                var info = type.GetProperty(part);
                if (info == null) return;
                info.SetValue(obj, value);
            }
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }
    }
}
