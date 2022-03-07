namespace System
{
    public static class StringExtensions
    {
        public static bool NullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
