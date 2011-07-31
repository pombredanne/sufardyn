using System;

namespace Test
{
    /// <summary>
    /// Logging Timer
    /// </summary>
    public class Timer : IDisposable
    {
        private readonly DateTime value;

        public Timer()
        {
            value = DateTime.Now;
        }

        public void Dispose()
        {
            Console.Out.WriteLine(@"Time = {0}ms", (DateTime.Now - value).TotalMilliseconds);
        }
    }
}