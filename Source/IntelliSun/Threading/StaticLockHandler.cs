using System;

namespace IntelliSun.Threading
{
    internal class StaticLockHandler : ILockHandler
    {
        private static readonly ILockHandler _instance;

        static StaticLockHandler()
        {
            StaticLockHandler._instance = new StaticLockHandler();
        }

        public void Exit()
        {
            
        }

        public void Dispose()
        {
            
        }

        public bool IsAcquired
        {
            get { return true; }
        }

        public static ILockHandler Instance
        {
            get { return StaticLockHandler._instance; }
        }
    }
}