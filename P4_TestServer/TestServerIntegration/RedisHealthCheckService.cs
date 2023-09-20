using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestServerIntegration;

public class RedisHealthCheckService : AbstractDependencyService
{
    protected override TimeSpan WorkDelay => TimeSpan.FromSeconds(3);
}