using System;

namespace IntelliSun.Text
{
    public interface IConsoleWriterContext
    {
        IConsoleWriterContext Write(string value);
        IConsoleWriterContext Write(object value);
        IConsoleWriterContext Write(string format, object arg);
        IConsoleWriterContext Write(string format, params object[] args);

        IConsoleWriterContext WriteLine();
        IConsoleWriterContext WriteLine(string value);
        IConsoleWriterContext WriteLine(object value);
        IConsoleWriterContext WriteLine(string format, object arg);
        IConsoleWriterContext WriteLine(string format, params object[] args);

        IConsoleWriterContext Write(ConsoleColor color, string value);
        IConsoleWriterContext Write(ConsoleColor color, object value);
        IConsoleWriterContext Write(ConsoleColor color, string format, object arg);
        IConsoleWriterContext Write(ConsoleColor color, string format, params object[] args);

        IConsoleWriterContext WriteLine(ConsoleColor color, string value);
        IConsoleWriterContext WriteLine(ConsoleColor color, object value);
        IConsoleWriterContext WriteLine(ConsoleColor color, string format, object arg);
        IConsoleWriterContext WriteLine(ConsoleColor color, string format, params object[] args);

        IConsoleWriterContext Tab(Action<IConsoleWriterContext> context);
        IConsoleWriterContext Tabs(uint count, Action<IConsoleWriterContext> context);

        IConsoleWriterContext Root { get; }
        IConsoleWriterContext Parent { get; }
    }
}