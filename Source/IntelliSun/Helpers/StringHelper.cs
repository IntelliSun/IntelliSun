using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using IntelliSun.Collections;

namespace IntelliSun.Helpers
{
    public static class StringHelper
    {
        public static string Truncate(this string value, int length)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return value.Length <= length ? value : value.Substring(0, length);
        }

        public static string SelectRange(this string i, char first, char last)
        {
            return SelectRange(i, first, last, 0);
        }

        public static string SelectRange(this string i, char first, char last, int startIndex)
        {
            var idx1 = i.IndexOf(first, startIndex) + 1;
            var idx2 = i.IndexOf(last, idx1);

            if (idx1 == 0 || idx2 == -1)
                return null;

            return i.Substring(idx1, idx2 - idx1);
        }

        public static bool StartsWithAny(this string self, params string[] value)
        {
            return value.Any(self.StartsWith);
        }

        public static bool StartsWithAny(this string self, StringComparison comparisonType, params string[] value)
        {
            return value.Any(x => self.StartsWith(x, comparisonType));
        }

        public static bool StartsWithAny(this string self, bool ignoreCase, CultureInfo culture, params string[] value)
        {
            return value.Any(x => self.StartsWith(x, ignoreCase, culture));
        }


        public static bool EndsWith(this string text, IEnumerable<string> strings)
        {
            return strings.Any(text.EndsWith);
        }

        public static bool EndsWith(this string text, IEnumerable<string> strings, bool ignoreCase)
        {
            return strings.Any(s => text.EndsWith(s, ignoreCase, CultureInfo.InvariantCulture));
        }

        public static bool EqualsAny(this string text, params string[] strings)
        {
            return text.EqualsAny(strings, false);
        }

        public static bool EqualsAny(this string text, string[] strings, bool ignoreCase)
        {
            return text.EqualsAny((IEnumerable<string>)strings, false);
        }

        public static bool EqualsAny(this string text, IEnumerable<string> strings)
        {
            return text.EqualsAny(strings, false);
        }

        public static bool EqualsAny(this string text, IEnumerable<string> strings, bool ignoreCase)
        {
            var comparison = ignoreCase
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            return strings.Any(s => text.Equals(s, comparison));
        }

        public static bool EqualsTo(this string source, string value, bool ignoreCase)
        {
            var comparison = ignoreCase
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            return source.Equals(value, comparison);
        }

        public static string RemoveLast(this string i)
        {
            return RemoveLast(i, 1);
        }

        public static string RemoveLast(this string i, int count)
        {
            return i.Remove(i.Length - count);
        }

        public static string RemoveSuffix(this string text, string suffix)
        {
            return text.EndsWith(suffix) ? text.RemoveLast(suffix.Length) : text;
        }

        public static string RemoveAnySuffix(this string text, params string[] suffixes)
        {
            return RemoveAnySuffix(text, (IEnumerable<string>)suffixes);
        }

        public static string RemoveAnySuffix(this string text, IEnumerable<string> suffixes)
        {
            var suffix = suffixes.SingleOrDefault(text.EndsWith);
            return suffix == null ? text : RemoveSuffix(text, suffix);
        }

        public static bool IsInteger(this string value)
        {
            long _;
            return Int64.TryParse(value, out _);
        }

        public static bool IsDecimal(this string value)
        {
            double _;
            return Double.TryParse(value, out _);
        }

        [Obsolete("Use 'IsInteger' or 'IsDecimal' instead")]
        public static bool IsNumber(this string i)
        {
            int _;
            return Int32.TryParse(i, out _);
        }

        [Obsolete("Use 'IsInteger' or 'IsDecimal' instead")]
        public static bool IsNumber(this string i, bool acceptDecimal)
        {
            return i.IsNumber(true, acceptDecimal);
        }

        [Obsolete("Use 'IsInteger' or 'IsDecimal' instead")]
        public static bool IsNumber(this string i, bool acceptDecimal, bool acceptComma)
        {
            decimal rn;

            if (acceptComma)
                i = i.Replace(",", "");

            return Decimal.TryParse(i, out rn);
        }

        public static string[] SplitWords(this string text)
        {
            return SplitWordsCore(text).ToArray();
        }

        private static IEnumerable<string> SplitWordsCore(this string text)
        {
            var builder = new StringBuilder();
            foreach (var c in text)
            {
                if (Char.IsUpper(c) || c == '-' || c == ' ' || c == '_')
                {
                    var word = builder.ToString();
                    builder.Clear();

                    if (!String.IsNullOrEmpty(word))
                        yield return word;
                }

                builder.Append(c);
            }

            var lastWord = builder.ToString();
            if (!String.IsNullOrEmpty(lastWord))
                yield return lastWord;
        }

        [Obsolete("Use split words instead")]
        public static string[] SplitCasing(this string text)
        {
            return InternalSplitCasing(text).ToArray();
        }

        private static IEnumerable<string> InternalSplitCasing(string text)
        {
            var wordCast = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (Char.IsUpper(text[i]) && i != 0)
                {
                    yield return wordCast.ToString();
                    wordCast.Clear();
                }

                wordCast.Append(text[i]);
            }

            if (wordCast.Length > 0)
                yield return wordCast.ToString();
        }

        public static string Cut(this string i, int fromEdges)
        {
            return i.Cut(fromEdges, fromEdges);
        }

        public static string Cut(this string i, int fromStart, int fromEnd)
        {
            if (fromStart < 0 || fromStart >= i.Length)
                throw new ArgumentOutOfRangeException("fromStart");
            if (fromEnd < 0 || fromEnd >= i.Length)
                throw new ArgumentOutOfRangeException("fromEnd");

            return i.Substring(fromStart, i.Length - fromEnd - fromStart);
        }

        public static string Wrap(this string i, char c)
        {
            return i.Wrap(c, c);
        }

        public static string Wrap(this string i, WrappingSet set)
        {
            var data = "\0\0";
            switch (set)
            {
                case WrappingSet.Parentheses:
                    data = "()";
                    break;
                case WrappingSet.SquareBrackets:
                    data = "[]";
                    break;
                case WrappingSet.Bracket:
                    data = "{}";
                    break;
                case WrappingSet.DoubleQuotes:
                    data = "\"\"";
                    break;
                case WrappingSet.SingleQuotes:
                    data = "''";
                    break;
            }

            return String.Concat(data[0], i, data[1]);
        }

        public static string Wrap(this string i, char cFirst, char cLast)
        {
            return String.Concat(cFirst, i, cLast);
        }

        public static bool Contains(this string i, IEnumerable<char> value)
        {
            return value.Any(i.Contains);
        }

        public static bool Contains(this string i, IEnumerable<string> value)
        {
            return value.Any(i.Contains);
        }

        public static string ConcatWith(string format, string str1)
        {
            return ConcatWith(format, new object[] { str1 });
        }

        public static string ConcatWith(string format, object arg1, object arg2)
        {
            return ConcatWith(format, new[] { arg1, arg2 });
        }

        public static string ConcatWith(string format, string str1, string str2)
        {
            return ConcatWith(format, new object[] { str1, str2 });
        }

        public static string ConcatWith(string format, IEnumerable<string> values)
        {
            return ConcatWith(format, values.Cast<object>());
        }

        public static string ConcatWith<T>(string format, IEnumerable<T> values)
        {
            return ConcatWith(format, values.Cast<object>());
        }

        public static string ConcatWith(string format, params string[] args)
        {
            return ConcatWith(format, args.Cast<object>());
        }

        public static string ConcatWith(string format, IEnumerable<object> args)
        {
            return ConcatWith(format, args.ToArray());
        }

        public static string ConcatWith(string format, params object[] args)
        {
            return SequentialFormatter.Instance.Format(format, args);
        }

        public static string FirstAs(this string value, CaseOption option)
        {
            if (String.IsNullOrEmpty(value))
                return value;

            var ch = option == CaseOption.Lower
                         ? Char.ToLower(value[0])
                         : Char.ToUpper(value[0]);

            return String.Concat(ch, value.Substring(1));
        }

        public static string ReformatWords(this string value, CasingOption option)
        {
            return value.ReformatWords(option, ' ');
        }

        public static string ReformatWords(this string value, CasingOption option, char splitter)
        {
            return ReformatWords(value.Split(splitter), option);
        }

        public static string ReformatWords(this string value, CasingOption option, string splitter)
        {
            return ReformatWords(value.Split(new[] { splitter }, StringSplitOptions.None), option);
        }

        public static string ReformatWords(string[] words, CasingOption option)
        {
            if(words == null || words.IsEmpty())
                return String.Empty;

            var firstOption = option == CasingOption.AllUpper ||
                              option == CasingOption.FirstUpper ||
                              option == CasingOption.UpperCamelCase
                                  ? CaseOption.Upper
                                  : CaseOption.Lower;

            var otherOption = option == CasingOption.AllUpper ||
                              option == CasingOption.LowerCamelCase ||
                              option == CasingOption.UpperCamelCase
                                  ? CaseOption.Upper
                                  : CaseOption.Lower;

            var separator = (int)option < 10 ? "_" : "";
            var builder = new StringBuilder(words[0].FirstAs(firstOption));
            foreach (var word in words.Skip(1))
                builder.Append(String.Concat(separator, word.FirstAs(otherOption)));

            return builder.ToString();
        }

        public static string[] Split(string value, Func<char, bool> splitter)
        {
            return SplitInternal(value, splitter).ToArray();
        }

        private static IEnumerable<string> SplitInternal(string value, Func<char, bool> splitter)
        {
            if (String.IsNullOrEmpty(value))
                yield break;

            var splitIndex = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (!splitter(value[i]))
                    continue;

                yield return value.Substring(splitIndex, i - splitIndex);
                splitIndex = i + 1;
            }

            yield return value.Substring(splitIndex);
        }

        public enum WrappingSet
        {
            Parentheses,
            SquareBrackets,
            Bracket,
            DoubleQuotes,
            SingleQuotes
        }
    }
}
