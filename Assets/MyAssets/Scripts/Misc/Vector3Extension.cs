using System.Globalization;
using UnityEngine;

namespace Misc
{
    public static class Vector3ExtensionMethods
    {
        public static string ToParsableString(this Vector3 v) => v.x.ToString(CultureInfo.InvariantCulture) + "," + v.y.ToString(CultureInfo.InvariantCulture) + "," + v.z.ToString(CultureInfo.InvariantCulture);

        public static Vector3 ToVector3(this string str)
        {
            string[] parts = str.Split(',');
            return new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
        }
    }
}
