using System;

namespace IntelliSun.Threading
{
    public interface ILockHandler : IDisposable
    {
        void Exit();

        bool IsAcquired { get; }
    }
}