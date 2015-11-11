using System;

namespace IntelliSun
{
    public struct VersionConstraint
    {
        private readonly ValueRange major;
        private readonly ValueRange minor;
        private readonly ValueRange build;
        private readonly ValueRange revision;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public VersionConstraint(ValueRange major)
            : this(major, ValueRange.Any())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public VersionConstraint(ValueRange major, ValueRange minor)
            : this(major, minor, ValueRange.Any())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public VersionConstraint(ValueRange major, ValueRange minor, ValueRange build)
            : this(major, minor, build, ValueRange.Any())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public VersionConstraint(ValueRange major, ValueRange minor, ValueRange build, ValueRange revision)
        {
            this.major = major;
            this.minor = minor;
            this.build = build;
            this.revision = revision;
        }

        public bool IsValid(Version version)
        {
            return this.major.IsValid(version.Major) &&
                   this.minor.IsValid(version.Minor) &&
                   this.build.IsValid(version.Build) &&
                   this.revision.IsValid(version.Revision);
        }
    }
}