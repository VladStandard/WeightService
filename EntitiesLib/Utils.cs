using System.IO;

namespace EntitiesLib
{
    public static class Utils
    {
        public static void StringValueTrim(ref string value, int length, bool isGetFileName = false)
        {
            if (isGetFileName)
                value = Path.GetFileName(value);
            if (value.Length > length)
                value = value.Substring(0, length);
        }
    }
}
