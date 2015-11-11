using System;
using System.IO;

namespace IntelliSun.IO
{
    internal class RollStream : Stream
    {
        private readonly Stream streamBase;

        public RollStream(Stream streamBase)
        {
            this.streamBase = streamBase;
        }

        public override void Flush()
        {
            this.streamBase.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.streamBase.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.streamBase.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.streamBase.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.streamBase.Write(buffer, offset, count);
        }

        public new void Dispose()
        {
            this.streamBase.Position = 0;
            this.IsFree = true;
        }

        internal void DisposeAll()
        {
            this.streamBase.Dispose();
            this.Dispose(true);
        }

        public override bool CanRead
        {
            get { return this.streamBase.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.streamBase.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { return this.streamBase.Length; }
        }

        public override long Position
        {
            get { return this.streamBase.Position; }
            set { this.streamBase.Position = value; }
        }

        internal bool IsFree { get; set; }
    }
}