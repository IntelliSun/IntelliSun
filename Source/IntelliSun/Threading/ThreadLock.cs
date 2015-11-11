using System;

namespace IntelliSun.Threading
{
    public abstract class ThreadLock : IThreadLock
    {
        public abstract ILockHandler LockRead();
        public abstract ILockHandler LockRead(bool blocking);

        public abstract ILockHandler LockWrite();
        public abstract ILockHandler LockWrite(bool blocking);

        public static ThreadLock Create()
        {
            return new SlimThreadLock();
        }
    }
}
