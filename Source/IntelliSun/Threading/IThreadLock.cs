using System;

namespace IntelliSun.Threading
{
    public interface IThreadLock
    {
        ILockHandler LockRead();
        ILockHandler LockRead(bool blocking);

        ILockHandler LockWrite();
        ILockHandler LockWrite(bool blocking);
    }
}