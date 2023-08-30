namespace FlexibleData.Application.Extensions
{
    public static class ListExtensions
    {
        public static void AddIfNotNull<T>(this List<T> list, IEnumerable<T> itemList)
        {
            if (itemList != null)
            {
                list.AddRange(itemList);
            }
        }

        public static void AddIfNotNull<T>(this List<T> list, T item)
        {
            if (item != null)
            {
                list.Add(item);
            }
        }
    }
}
