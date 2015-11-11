using System;

namespace IntelliSun.Text
{
    internal class FormatTokenDelegate : IFormatTokenDelegate
    {
        private readonly Func<string, string> provider;

        public FormatTokenDelegate(Func<string, string> provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            this.provider = provider;
        }

        public string GetString(string token)
        {
            return this.provider(token);
        }
    }
}