using System;

namespace IntelliSun
{
    public sealed class CallDisposable : IDisposable
    {
        private readonly Action call;

        public CallDisposable(Action call)
        {
            if (call == null)
                throw new ArgumentNullException("call");

            this.call = call;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.call();
        }
    }
}