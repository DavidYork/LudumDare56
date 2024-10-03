public static class Extensions {
    public static bool IsNil(this string self) => string.IsNullOrWhiteSpace(self);
}