using System;
using System.Threading;

namespace IntelliSun.Threading
{
    internal class SlimThreadLock : ThreadLock
    {
        private const LockRecursionPolicy DefaultRecursionPolicy =
            LockRecursionPolicy.NoRecursion;

        private readonly ReaderWriterLockSlim lockInstance;

        public SlimThreadLock()
        {
            this.lockInstance = new ReaderWriterLockSlim(SlimThreadLock.DefaultRecursionPolicy);
        }

        public override ILockHandler LockRead()
        {
            return this.LockRead(true);
        }

        public override ILockHandler LockRead(bool blocking)
        {
            return this.SouldExecuteLock(false)
                ? StaticLockHandler.Instance
                : SlimThreadLockHandler.ReadLock(this.lockInstance, blocking);
        }

        public override ILockHandler LockWrite()
        {
            return this.LockWrite(true);
        }

        public override ILockHandler LockWrite(bool blocking)
        {
            return this.SouldExecuteLock(true)
                ? StaticLockHandler.Instance
                : SlimThreadLockHandler.WriteLock(this.lockInstance, blocking);
        }

        private bool SouldExecuteLock(bool asWrite)
        {
            return (asWrite && this.lockInstance.IsWriteLockHeld) ||
                   (!asWrite && this.lockInstance.IsReadLockHeld ||
                    this.lockInstance.IsUpgradeableReadLockHeld ||
                    this.lockInstance.IsWriteLockHeld);
        }
    }
}