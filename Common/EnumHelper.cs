namespace SuDoKu.Common
{
    public static class EnumHelper
    {
        public static T GetNextItem<T>(this T item, T[] items)
        {
            var nextIndex = 0;
            var maxIndex = items.Length;
            for (int i = 1; (i <= maxIndex) && items[i].Equals(item); i++)
            {
                nextIndex = (i % maxIndex) + 1;
            }
            return items[nextIndex];
        }

        public static T GetFirstItem<T>(this T[] items) => items[0];

        public static T GetLastItem<T>(this T[] items) => items[items.Length - 1];
    }
}
