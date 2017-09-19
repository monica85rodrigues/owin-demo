namespace Pluralsight.Owin.Demo.Middleware
{
    using Microsoft.Owin;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using AppFunc = System.Func<
        System.Collections.Generic.IDictionary<string, object>,
        System.Threading.Tasks.Task
    >;

    public class DebugMiddleware
    {
        AppFunc next;
        DebugMiddlewareOptions options;

        public DebugMiddleware(AppFunc next, DebugMiddlewareOptions options)
        {
            this.next = next;
            this.options = options;

            if (this.options.OnIncomingRequest == null)
                this.options.OnIncomingRequest = (ctx) => { Debug.WriteLine("Incoming request: " + ctx.Request.Path); };

            if (this.options.OnOutgoingRequest == null)
                this.options.OnOutgoingRequest = (ctx) => { Debug.WriteLine("Outgoing request: " + ctx.Request.Path); };
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var ctx = new OwinContext(environment);

            this.options.OnIncomingRequest(ctx);
            await next(environment);
            this.options.OnOutgoingRequest(ctx);
        }
    }
}