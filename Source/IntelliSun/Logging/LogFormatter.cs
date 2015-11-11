using System;
using System.Diagnostics;
using System.Globalization;
using IntelliSun.Text;

namespace IntelliSun.Logging
{
    public static class LogFormatter
    {
        public static string Format(string template, ILogger logger, string message, Exception exception, string category, LogLevel level)
        {
            return new TokenFormatter(new[] {
                FormatToken.New("time", () => DateTime.Now.ToLongTimeString()),
                FormatToken.New("date", () => DateTime.Now.ToLongDateString()),
                FormatToken.New("type", () => logger.ContainerType.Name),
                FormatToken.New("typef", () => logger.ContainerType.FullName),
                FormatToken.New("prcc", () => Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture)),
                FormatToken.New("ticks", () => Environment.TickCount.ToString(CultureInfo.InvariantCulture)),
                FormatToken.New("msg", () => message),
                FormatToken.New("ctr", () => category),
                FormatToken.New("lvl", () => level.ToString())
            }, "{$$}").Format(template);
        }
    }
}
