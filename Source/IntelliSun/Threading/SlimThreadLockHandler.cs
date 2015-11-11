using System;
using System.Threading;

namespace IntelliSun.Threading
{
    internal class SlimThreadLockHandler : ILockHandler
    {
        private readonly ReaderWriterLockSlim lockInstance;
        private readonly bool isWriteLock;

        private SlimThreadLockHandler(ReaderWriterLockSlim lockInstance, bool isWriteLock, bool blocking)
        {
            this.lockInstance = lockInstance;
            this.isWriteLock = isWriteLock;

            this.Enter(blocking);
        }

        public void Exit()
        {
            if (!this.IsAcquired)
                return;

            this.ExitCore();
            this.IsAcquired = false;
        }

        private void ExitCore()
        {
            if (this.isWriteLock)
                this.lockInstance.ExitWriteLock();
            else
                this.lockInstance.ExitReadLock();
        }

        private void Enter(bool blocking)
        {
            this.IsAcquired = blocking
                ? this.EnterBlocking()
                : this.EnterNonBlocking();
        }

        private bool EnterNonBlocking()
        {
            return this.isWriteLock
                ? this.lockInstance.TryEnterWriteLock(0)
                : this.lockInstance.TryEnterReadLock(0);
        }

        private bool EnterBlocking()
        {
            if (this.isWriteLock)
                this.lockInstance.EnterWriteLock();
            else
                this.lockInstance.EnterReadLock();

            return true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Exit();
        }

        public static SlimThreadLockHandler ReadLock(ReaderWriterLockSlim locker, bool blocking)
        {
            return new SlimThreadLockHandler(locker, false, blocking);
        }

        public static SlimThreadLockHandler WriteLock(ReaderWriterLockSlim locker, bool blocking)
        {
            return new SlimThreadLockHandler(locker, true, blocking);
        }

        public bool IsAcquired { get; private set; }
    }
}