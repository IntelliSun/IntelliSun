using System;

namespace IntelliSun.Text
{
    public class ConsoleWriter
    {
        private readonly ConsoleWriterConfig config;
        private readonly IConsoleWriterContext root;

        public ConsoleWriter(ConsoleWriterConfig config)
        {
            this.config = config;
            this.root = new ConsoleWriterContext(null, this.config);
        }

        public ConsoleWriterConfig Config
        {
            get { return this.config; }
        }

        public IConsoleWriterContext Root
        {
            get { return this.root; }
        }
    }
}
