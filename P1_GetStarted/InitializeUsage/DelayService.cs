using System;
using System.Threading;
using System.Threading.Tasks;

namespace InitializeUsage
{
    public class DelayService
    {
        private long _readyStatus;

        public DelayService()
        {
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                Interlocked.Exchange(ref _readyStatus, 1);
            });
        }

        public bool Ready()
        {
            return Interlocked.Read(ref _readyStatus) == 1;
        }

        public void Test()
        {
            if (!Ready())
            {
                throw new InvalidOperationException();
            }
        }
    }
}