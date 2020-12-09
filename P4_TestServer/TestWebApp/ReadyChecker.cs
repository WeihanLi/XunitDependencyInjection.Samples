using System.Threading.Tasks;

namespace TestWebApp
{
    public class ReadyChecker
    {
        private static int _readyStatus;

        static ReadChecker()
        {
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                _readyStatus = 1;
            });
        }

        public bool Check()
        {
            return _readyStatus > 0;
        }
    }
}