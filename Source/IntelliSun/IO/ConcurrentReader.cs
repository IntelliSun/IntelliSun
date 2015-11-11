using System;
using System.IO;
using System.Linq;
using IntelliSun.Collections;

namespace IntelliSun.IO
{
    public class ConcurrentReader : Unmanaged
    {
        private readonly Func<Stream> factory;
        private readonly int capacity;

        private readonly Stream[] permanents;
        private readonly RollStream[] rollStreams;

        private BinaryReader permanentReader;

        public ConcurrentReader(Func<Stream> factory)
            : this(factory, 1, 1)
        {
            
        }

        public ConcurrentReader(Func<Stream> factory, int capacity)
            : this(factory, capacity, 1)
        {
        }

        public ConcurrentReader(Func<Stream> factory, int capacity, int permanents)
        {
            this.factory = factory;
            this.capacity = capacity;

            this.rollStreams = EnumerationBuilder.RepeatUnique(() => new RollStream(factory()), capacity)
                                                 .ToArray();

            this.permanents = EnumerationBuilder.RepeatUnique(factory, permanents)
                                                .ToArray();
        }

        public Stream Get()
        {
            var stream = this.rollStreams.FirstOrDefault(s => s.IsFree);
            return stream ?? this.GetOnce();
        }

        public void Free(Stream stream)
        {
            stream.Dispose();
        }

        public Stream GetOnce()
        {
            using (var stream = factory())
                return stream;
        }

        public Stream GetUnmanaged()
        {
            return factory();
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var permanent in permanents)
                permanent.Dispose();

            foreach (var rollStream in rollStreams)
                rollStream.DisposeAll();

            base.Dispose(disposing);
        }

        public Stream Permanent
        {
            get { return this.permanents[0]; }
        }

        public BinaryReader PermanentReader
        {
            get { return permanentReader ?? (permanentReader = new BinaryReader(this.permanents[0])); }
        }

        public Stream this[int index]
        {
            get
            {
                return index < this.permanents.Length
                    ? this.permanents[index]
                    : this.rollStreams[index - this.permanents.Length];
            }
        }

        public int Capacity
        {
            get { return this.capacity; }
        }
    }
}
