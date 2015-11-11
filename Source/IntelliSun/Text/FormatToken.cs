using System;

namespace IntelliSun.Text
{
    public class FormatToken
    {
        private readonly string token;
        private readonly IFormatTokenDelegate formatDelegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        private FormatToken(string token, IFormatTokenDelegate formatDelegate)
        {
            this.token = token;
            this.formatDelegate = formatDelegate;
        }

        public string GetString(string token)
        {
            return this.formatDelegate.GetString(token);
        }

        public static FormatToken New(string token, Func<string> formatDelegate)
        {
            return New(token, _ => formatDelegate());
        }

        public static FormatToken New(string token, Func<string, string> formatDelegate)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            if (formatDelegate == null)
                throw new ArgumentNullException("formatDelegate");

            return new FormatToken(token, new FormatTokenDelegate(formatDelegate));
        }

        public string Token
        {
            get { return this.token; }
        }
    }
}