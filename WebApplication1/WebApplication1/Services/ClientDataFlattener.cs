using System.Collections;
using System.Reflection;

namespace WebApplication1.Services
{
    public static class ClientDataFlattener
    {
        public static Dictionary<string, object> Flatten(object data, string prefix = "")
        {
            var result = new Dictionary<string, object>();
            FlattenRecursive(data, prefix, result);
            return result;
        }

        private static void FlattenRecursive(object data, string prefix, Dictionary<string, object> result)
        {
            if (data == null) return;

            var type = data.GetType();

            // If it's a primitive or string, store it
            if (type.IsPrimitive || data is string || data is decimal)
            {
                result[prefix] = data;
                return;
            }

            // If it's a dictionary
            if (data is IDictionary dict)
            {
                foreach (DictionaryEntry entry in dict)
                {
                    var key = entry.Key.ToString();
                    var value = entry.Value;
                    FlattenRecursive(value, JoinKey(prefix, key), result);
                }
                return;
            }

            // If it's a complex object, recurse through properties
            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = prop.GetValue(data);
                var key = prop.Name;
                FlattenRecursive(value, JoinKey(prefix, key), result);
            }
        }

        private static string JoinKey(string prefix, string key)
        {
            return string.IsNullOrEmpty(prefix) ? key : $"{prefix}.{key}";
        }
    }
}
