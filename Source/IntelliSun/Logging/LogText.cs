using System;
using System.Collections.Generic;
using IntelliSun.Helpers;
using IntelliSun.Text;

namespace IntelliSun.Logging
{
    public class LogText
    {
        private readonly string message;

        public LogText(string message)
        {
            this.message = message;
        }

        public string Resolve(ILogger logger)
        {
            return new TokenFormatter(new[] {
                FormatToken.New("t", () => logger.ContainerType.Name),
                FormatToken.New("q", () => logger.ContainerType.FullName)
            }).Format(this.message);
        }

        public string Message
        {
            get { return this.message; }
        }
    }
}
