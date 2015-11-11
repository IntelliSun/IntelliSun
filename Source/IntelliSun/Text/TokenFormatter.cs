using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntelliSun.Collections;

namespace IntelliSun.Text
{
    public class TokenFormatter
    {
        public const string TemplateToken = "$$";
        private const string DefaultTemplate = "-{$$}";

        private readonly string template;
        private readonly Dictionary<string, FormatToken> tokensMap;

        public TokenFormatter()
            : this(TokenFormatter.DefaultTemplate)
        {
        }

        public TokenFormatter(string template)
            : this(new Dictionary<string, FormatToken>(), template) 
        {
            
        }

        public TokenFormatter(IEnumerable<FormatToken> tokens)
            : this(tokens, TokenFormatter.DefaultTemplate)
        {
        }

        public TokenFormatter(IEnumerable<FormatToken> tokens, string template)
            : this(Initialize(tokens), template)
        {
            
        }

        private TokenFormatter(Dictionary<string, FormatToken> tokens, string template)
        {
            if (template == null)
                throw new ArgumentNullException("template");

            this.template = template;
            this.tokensMap = tokens;
        }

        /// <summary>
        /// Add a new set of replacement tokens.
        /// </summary>
        /// <param name="tokens">Tokens to add to the list.</param>
        public void Add(params FormatToken[] tokens)
        {
            this.AddRange(tokens);
        }

        /// <summary>
        /// Add new tokens to the set of replacements.
        /// </summary>
        /// <param name="tokens">Tokens to add to the list.</param>
        public void AddRange(IEnumerable<FormatToken> tokens)
        {
            this.tokensMap.Map(tokens, t => t.Token, t => t);
        }

        public string Format(string format)
        {
            var result = new StringBuilder(format);
            foreach (var formatToken in this.tokensMap)
            {
                var token = this.BuildToken(formatToken.Value);
                result.Replace(token, formatToken.Value.GetString(format));
            }

            return result.ToString();
        }

        private string BuildToken(FormatToken token)
        {
            return this.template.Replace(TokenFormatter.TemplateToken, token.Token);
        }

        private static KeyValuePair<string, FormatToken> Map(FormatToken token)
        {
            return new KeyValuePair<string, FormatToken>(token.Token, token);
        }

        private static IEnumerable<KeyValuePair<string, FormatToken>> Map(IEnumerable<FormatToken> tokens)
        {
            return tokens.Select(Map);
        }

        private static Dictionary<string, FormatToken> Initialize(IEnumerable<FormatToken> tokens)
        {
            return Map(tokens).ToDictionary();
        }

        #region Interactive Formtting -> TokenFormatter

        //public static string InteractiveFormat(string format, Func<string, string> accessor)
        //{
        //    return InteractiveFormat(format, accessor, "-{0}");
        //}

        //public static string InteractiveFormat(string format, Func<string, string> accessor, string template)
        //{
        //    if (String.IsNullOrEmpty(format))
        //        return String.Empty;

        //    if (accessor == null)
        //        return format;

        //    if (String.IsNullOrEmpty(template))
        //        template = "-{0}";

        //    if (template.Count(x => x.Equals('0')) != 1)
        //        throw new FormatException("Format template must contain one content symbol");

        //    var templateParts = template.Split('0');
        //    var templateStart = templateParts[0];
        //    var templateEnd = templateParts[1];
        //    var templateLevel = 0;

        //    var contentCollector = new StringBuilder();
        //    var stringBuilder = new StringBuilder();
        //    var pendingCollector = new StringBuilder();
        //    var idx = 0;
        //    while (idx < format.Length)
        //    {
        //        var cast = format[idx++];
        //        if (templateLevel != templateStart.Length)
        //        {
        //            if (cast == templateStart[templateLevel])
        //                pendingCollector.Append(templateStart[templateLevel++]);
        //            else
        //            {
        //                if (templateLevel > 0)
        //                {
        //                    templateLevel = 0;
        //                    stringBuilder.Append(pendingCollector);
        //                    pendingCollector.Clear();
        //                }
        //                stringBuilder.Append(cast);
        //            }
        //        }
        //        else
        //        {
        //            if (cast == templateEnd[0])
        //            {
        //                templateLevel = 0;
        //                var val = contentCollector.ToString();
        //                stringBuilder.Append(accessor(val));
        //                contentCollector.Clear();
        //                continue;
        //            }

        //            contentCollector.Append(cast);
        //        }
        //    }

        //    return stringBuilder.ToString();
        //}

        //public static string InteractiveFormat(string format, InteractiveFormatParams parameters)
        //{
        //    if (String.IsNullOrEmpty(format))
        //        return String.Empty;

        //    if (parameters.Accessor == null)
        //        return format;

        //    if (String.IsNullOrEmpty(parameters.Template))
        //        parameters.Template = "-{0}";

        //    if (parameters.Template.Count(x => x.Equals('0')) != 1)
        //        throw new FormatException("Format template must contain only one content symbol");

        //    var templateParts = parameters.Template.Split('0');
        //    var templateStart = templateParts[0];
        //    var templateEnd = templateParts[1];
        //    var templateLevel = 0;

        //    var contentCollector = new StringBuilder();
        //    var stringBuilder = new StringBuilder();
        //    var pendingCollector = new StringBuilder();
        //    var idx = 0;
        //    while (idx < format.Length)
        //    {
        //        var cast = format[idx++];
        //        if (templateLevel != templateStart.Length)
        //        {
        //            if (cast == templateStart[templateLevel])
        //                pendingCollector.Append(templateStart[templateLevel++]);
        //            else
        //            {
        //                if (templateLevel > 0)
        //                {
        //                    templateLevel = 0;
        //                    stringBuilder.Append(pendingCollector);
        //                    pendingCollector.Clear();
        //                }
        //                stringBuilder.Append(cast);
        //            }
        //        }
        //        else
        //        {
        //            if (cast == templateEnd[0])
        //            {
        //                templateLevel = 0;
        //                var val = contentCollector.ToString();
        //                var par = parameters.ArgsPreset.FormKey(val);
        //                stringBuilder.Append(parameters.Accessor(par));
        //                contentCollector.Clear();
        //                continue;
        //            }

        //            contentCollector.Append(cast);
        //        }
        //    }

        //    return stringBuilder.ToString();
        //}

        #endregion
    }
}
