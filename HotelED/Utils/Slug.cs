namespace HotelED.Utils
{
    public static class Slug
    {
        public static string GenerateSlug(string name)
        {
            return name.ToLowerInvariant()
                .Replace(" ", "-")
                .Replace("’", "")
                .Replace("'", "")
                .Replace(",", "")
                .Replace(".", "");
        }
    }
}
