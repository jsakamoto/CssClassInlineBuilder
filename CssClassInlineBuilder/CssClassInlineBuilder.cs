using System.Collections.Concurrent;
using System.Reflection;
using System.Text;

namespace Toolbelt.Web;

/// <summary>
/// Build CSS class string for "class" attribute dynamically based on boolean switch values.
/// </summary>
public static partial class CssClassInlineBuilder
{
    /// <summary>
    /// Build CSS class string from boolean properties of objects in arguments, from strings in arguments.
    /// </summary>
    [Obsolete("Use \"CssClassInlineBuilder.V2.CssClass()\" instead since this method is incompatible with IL trimming.")]
    public static string CssClass(params object?[] args)
    {
        var builder = StringBuilderPool.Get();
        try
        {
            var _1st = true;
            foreach (var arg in args)
            {
                if (arg is string s)
                {
                    if (!_1st) builder.Append(' ');
                    _1st = false;
                    builder.Append(s);
                }
                else if (arg is Enum e)
                {
                    if (!_1st) builder.Append(' ');
                    _1st = false;
                    builder.Append(GetHyphenatedName(e.ToString()));
                }
                else if (arg is not null)
                {
                    foreach (var (name, getter, isBool) in GetPropEntriesFromCache(arg))
                    {
                        if (isBool)
                        {
                            if (getter?.Invoke(arg, null) is bool boolValue && boolValue)
                            {
                                if (!_1st) builder.Append(' ');
                                _1st = false;
                                builder.Append(name);
                            }
                        }
                        else
                        {
                            if (!_1st) builder.Append(' ');
                            _1st = false;
                            var value = getter?.Invoke(arg, null);
                            builder.Append(name + "-" + GetHyphenatedName(value?.ToString() ?? ""));
                        }
                    }
                }
            }

            return builder.ToString();
        }
        finally
        {
            StringBuilderPool.Return(builder);
        }
    }

    private class CacheEntry
    {
        public long Gen;
        public readonly IEnumerable<(string Name, MethodInfo? Getter, bool IsBool)> PropGetters;
        public CacheEntry((string Name, MethodInfo? Getter, bool IsBool)[] propGetters) { this.PropGetters = propGetters; }
    }

    private const int GCThreshold = 100;

    private const int GCKeepSize = 50;

    private static long Gen;

    private static int CountOfCache = 0;

    private static ConcurrentDictionary<Type, CacheEntry> Cache = new ConcurrentDictionary<Type, CacheEntry>();

    private static IEnumerable<(string Name, MethodInfo? Getter, bool IsBool)> GetPropEntriesFromCache(object arg)
    {
        var type = arg.GetType();
        if (Cache.TryGetValue(type, out var cache))
        {
            cache.Gen = Interlocked.Increment(ref Gen);
            return cache.PropGetters;
        }

        var propNamesAndGetters = type.GetProperties()
            .Select(p => (Name: GetHyphenatedName(p.Name), Getter: p.GetGetMethod(), IsBool: p.PropertyType == typeof(bool)))
            .ToArray();
        var newCache = new CacheEntry(propNamesAndGetters);
        newCache.Gen = Interlocked.Increment(ref Gen);

        if (Cache.TryAdd(type, newCache))
        {
            if (Interlocked.Increment(ref CountOfCache) >= GCThreshold)
            {
                lock (Cache)
                {
                    var toRemoves = Cache.OrderByDescending(kv => kv.Value.Gen).Skip(GCKeepSize).ToArray();
                    foreach (var toRemove in toRemoves)
                    {
                        if (Cache.TryRemove(toRemove.Key, out var _))
                            Interlocked.Decrement(ref CountOfCache);
                    }
                }
            }
        }

        return newCache.PropGetters;
    }

    private static string GetHyphenatedName(string baseName)
    {
        var name = baseName;
        unsafe
        {
            var buff = stackalloc char[name.Length * 2];
            var isPrevCharUpperCase = false;
            var j = 0;
            for (var i = 0; i < name.Length; i++)
            {
                var c = name[i];

                if (c == ' ')
                {
                    if (i + 1 < name.Length)
                    {
                        c = name[++i];
                        if ('a' <= c && c <= 'z')
                        {
                            c = (char)(((byte)c) & ~0x20);
                        }
                    }
                    else continue;
                }

                if ('A' <= c && c <= 'Z')
                {
                    if (!isPrevCharUpperCase && j != 0)
                    {
                        buff[j++] = '-';
                    }
                    buff[j] = (char)0x20;
                    isPrevCharUpperCase = true;
                }
                else isPrevCharUpperCase = false;
                buff[j++] |= c;
            }

            var hyphenatedName = new string(buff, 0, j);
            return hyphenatedName;
        }
    }
}

internal static class StringBuilderPool
{
    private static readonly ConcurrentBag<StringBuilder> BuilderPool = new ConcurrentBag<StringBuilder>();

    private static int CountOfBuilder = 0;

    private const int MaxOfBuilder = 10;

    internal static StringBuilder Get()
    {
        if (BuilderPool.TryTake(out var builder))
        {
            Interlocked.Decrement(ref CountOfBuilder);
            builder.Clear();
        }
        else
        {
            builder = new StringBuilder();
        }
        return builder;
    }

    internal static void Return(StringBuilder builder)
    {
        var count = Interlocked.Increment(ref CountOfBuilder);
        if (count <= MaxOfBuilder)
        {
            BuilderPool.Add(builder);
        }
        else
        {
            Interlocked.Decrement(ref CountOfBuilder);
        }
    }
}
