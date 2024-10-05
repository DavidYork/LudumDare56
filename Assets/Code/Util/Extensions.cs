using System;
using System.IO;
using Ink.Runtime;

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

    public static TEnum ToEnum<TEnum>(this InkList src) where TEnum: struct, Enum {
	    var values = src.Values;
	    var enumerator = values.GetEnumerator();
	    enumerator.MoveNext();
	    var val = enumerator.Current;

	    TEnum enumVal = (TEnum)Enum.ToObject(typeof(TEnum) , val - 1);
	    return enumVal;
	}

    // private

    static int pathCompare(string left, string right) {
        var leftFile = System.IO.Path.GetFileNameWithoutExtension(left);
        var rightFile = System.IO.Path.GetFileNameWithoutExtension(right);
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