using AspectCore.DynamicProxy;
using System.Threading.Tasks;

namespace AspectCoreIntegration
{
    public class CounterInterceptor : AbstractInterceptor
    {
        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            if (Helper.InterceptCounter <= 1)
            {
                Helper.InterceptCounter++;
                return next(context);
            }

            context.ReturnValue = null;
            return Task.CompletedTask;
        }
    }
}