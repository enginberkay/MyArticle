using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Content.Domain
{
    public static class Extensions
    {
        public static bool UpdatePropertyDifferences<T>(this T source, ref T destination)
        {
            bool changed = false;
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> changes = new List<string>();

            foreach (PropertyInfo pi in properties)
            {
                object value1 = typeof(T).GetProperty(pi.Name).GetValue(source, null);
                object value2 = typeof(T).GetProperty(pi.Name).GetValue(destination, null);
                string objectNamespace = typeof(T).GetProperty(pi.Name).PropertyType.Namespace;
                if (objectNamespace.Contains("Content."))
                    continue;
                if ("System.Collections.Generic" == objectNamespace)
                    continue;

                if (value1 != value2 && (value1 == null || !value1.Equals(value2)))
                {
                    typeof(T).GetProperty(pi.Name).SetValue(destination, value1);
                    changed = true;
                }
            }
            return changed;
        }
    }
}
