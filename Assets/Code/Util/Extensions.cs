#nullable disable
using System;
using System.IO;

public static class Extensions {
    public static bool IsNil(this string self) => string.IsNullOrWhiteSpace(self);

    public static string[] GetFilesSorted(this string path, string extension, EnumerationOptions options = null) {
        var files = options == null
            ? Directory.GetFiles(path, extension)
            : Directory.GetFiles(path, extension, options);
        files = Array.ConvertAll(files, f => f.Replace('\\', '/'));

        Array.Sort(files, (a, b) => pathCompare(a, b));
        return files;
    }

    // private

    static int pathCompare(string left, string right) {
        var leftFile = Path.GetFileNameWithoutExtension(left);
        var rightFile = Path.GetFileNameWithoutExtension(right);
        if (int.TryParse(leftFile.Split(' ')[0], out int leftVal) && int.TryParse(rightFile.Split(' ')[0],
            out int rightVal))
        {
            if (leftVal - rightVal != 0) {
                return leftVal - rightVal;
            }
        }
        var compare = string.Compare(left.ToLower(), right.ToLower());
        if (compare != 0) {
            return compare;
        }
        return string.Compare(left, right);
    }
}