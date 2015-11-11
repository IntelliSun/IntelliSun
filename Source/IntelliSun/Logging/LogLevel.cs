using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IntelliSun.Logging
{
    //public enum LogLevel
    //{
    //    All = 0,
    //    Debug = 1,
    //    Info = 2,
    //    Warning = 3,
    //    Error = 4,
    //    Critical = 5,
    //    Off = 0xFF
    //}

    public struct LogLevel : IEquatable<LogLevel>
    {
        /*
         * +----------+------------+
         * |  Level   | val(level) |
         * +----------+------------+
         * | Debug    |          0 |
         * | Trace    |         42 |
         * | Info     |         85 |
         * | Warning  |        127 |
         * | Error    |        170 |
         * | Critical |        212 |
         * | Fatal    |        255 |
         * +----------+------------+
         * */

        private const int LevelDebug = 0;
        private const string NameDebug = "Debug";

        private const int LevelTrace = 42;
        private const string NameTrace = "Trace";

        private const int LevelInfo = 85;
        private const string NameInfo = "Info";

        private const int LevelWarning = 127;
        private const string NameWarning = "Warning";

        private const int LevelError = 170;
        private const string NameError = "Error";

        private const int LevelCritical = 212;
        private const string NameCritical = "Critical";

        private const int LevelFatal = 255;
        private const string NameFatal = "Fatal";

        private readonly byte level;

        private LogLevel(byte level)
        {
            this.level = level;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(LogLevel other)
        {
            return this.level == other.level;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is LogLevel && this.Equals((LogLevel)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return this.level;
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return Cache.ToString(this);
        }

        public static LogLevel By(LogLevel baseLevel, int offset)
        {
            return By(baseLevel, offset, null);
        }

        public static LogLevel By(LogLevel baseLevel, int offset, string name)
        {
            if (Byte.MaxValue - baseLevel.level > offset)
                throw new OverflowException("${Resources.LogLevelOverflow}");

            if (-offset > baseLevel.level)
                throw new OverflowException("${Resources.LogLevelOverflow}");

            var newLevel = (byte)(baseLevel.level + offset);
            if (!String.IsNullOrEmpty(name))
                Cache.Register(newLevel, name);

            return new LogLevel(newLevel);
        }

        public static bool operator <(LogLevel left, LogLevel right)
        {
            return left.level < right.level;
        }

        public static bool operator >(LogLevel left, LogLevel right)
        {
            return left.level > right.level;
        }

        public static bool operator ==(LogLevel left, LogLevel right)
        {
            return left.level == right.level;
        }

        public static bool operator !=(LogLevel left, LogLevel right)
        {
            return !(left == right);
        }

        public static implicit operator LogLevel(byte level)
        {
            return new LogLevel(level);
        }

        public static LogLevel Debug
        {
            get { return new LogLevel(LevelDebug); }
        }

        public static LogLevel Trace
        {
            get { return new LogLevel(LevelTrace); }
        }

        public static LogLevel Info
        {
            get { return new LogLevel(LevelInfo); }
        }

        public static LogLevel Warning
        {
            get { return new LogLevel(LevelWarning); }
        }

        public static LogLevel Error
        {
            get { return new LogLevel(LevelError); }
        }

        public static LogLevel Critical
        {
            get { return new LogLevel(LevelCritical); }
        }

        public static LogLevel Fatal
        {
            get { return new LogLevel(LevelFatal); }
        }

        private static class Cache
        {
            private static readonly CacheProvider<byte> _nameCache;
            private static string _typeStringCache; 

            static Cache()
            {
                _nameCache = new CacheProvider<byte>(EqualityComparer<byte>.Default);
                InitializeDefaults();
            }

            private static void InitializeDefaults()
            {
                Register(LogLevel.LevelDebug, LogLevel.NameDebug);
                Register(LogLevel.LevelTrace, LogLevel.NameTrace);
                Register(LogLevel.LevelInfo, LogLevel.NameInfo);
                Register(LogLevel.LevelWarning, LogLevel.NameWarning);
                Register(LogLevel.LevelError, LogLevel.NameError);
                Register(LogLevel.LevelCritical, LogLevel.NameCritical);
                Register(LogLevel.LevelFatal, LogLevel.NameFatal);
            }

            public static void Register(byte logLevel, string name)
            {
                _nameCache.CacheData(logLevel, name);
            }

            public static string ToString(LogLevel logLevel)
            {
                var level = logLevel.level;
                var levelName = _nameCache.IsDataCached(level)
                    ? _nameCache[level]
                    : null;

                return levelName == null
                    ? String.Format("{0}({1})", GetTypeString(), level)
                    : (string)levelName;
            }

            private static string GetTypeString()
            {
                return _typeStringCache ?? (_typeStringCache = typeof(LogLevel).ToString());
            }
        }
    }
}