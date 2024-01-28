using System.Collections.Generic;

namespace Saving
{
    public static class SerializablePairListHelper
    {
        public static string GetValue(this List<SerializablePair> list, string key, string defaultValue = default)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Key == key)
                    return list[i].Value;
            }

            return defaultValue;
        }
    }
}