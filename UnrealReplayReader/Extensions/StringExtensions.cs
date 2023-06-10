using UnrealReplayReader.Models.Classes;

namespace UnrealReplayReader.Extensions;

public static class StringExtensions
{
    public static string? GetFullGuidPath(this NetworkGuid? guid)
    {
        if (guid == null)
        {
            return null;
        }

        if (guid.Outer != null)
        {
            return $"{guid.Outer.GetFullGuidPath()}.{guid.Path}";
        }

        return guid.Path ?? null;
    }

    public static string CleanStaticIdSuffix(this string id)
    {
        for (var i = id.Length - 1; i >= 0; i--)
        {
            var isNumber = id[i] >= '0' && id[i] <= '9';
            var isUnderscore = (id[i] == '_');

            if (!isNumber && !isUnderscore)
            {
                return id[..(i + 1)];
            }
        }

        return id;
    }


    public static string RemovePathPrefix(this string path, string toRemove = "")
    {
        while (true)
        {
            if (toRemove != "")
            {
                if (toRemove.Length > path.Length)
                {
                    return path;
                }

                for (var i = 0; i < toRemove.Length; i++)
                {
                    if (path[i] != toRemove[i])
                    {
                        return path;
                    }
                }

                return path[toRemove.Length..];
            }

            for (var i = path.Length - 1; i >= 0; i--)
            {
                switch (path[i])
                {
                    case '.':
                        return path[(i + 1)..];
                    case '/':
                        return path;
                }
            }

            toRemove = "Default__";
        }
    }
}