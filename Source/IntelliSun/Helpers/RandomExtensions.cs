using System;

namespace IntelliSun.Helpers
{
    public static class RandomExtensions
    {
        public static byte[] NextBytes(this Random random, uint count)
        {
            var buffer = new byte[count];
            random.NextBytes(buffer);

            return buffer;
        }
    }
}
