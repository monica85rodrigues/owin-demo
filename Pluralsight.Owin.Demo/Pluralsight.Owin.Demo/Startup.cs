namespace Pluralsight.Owin.Demo
{
    using global::Owin;
    using Pluralsight.Owin.Demo.Middleware;
    using System.Diagnostics;

    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            //second version
            app.UseDebugMiddleware(new DebugMiddlewareOptions
            {
                OnIncomingRequest = (ctx) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    ctx.Environment["DebugStopwatch"] = watch;
                },
                OnOutgoingRequest = (ctx) =>
                {
                    var watch = (Stopwatch)ctx.Environment["DebugStopwatch"];
                    watch.Stop();
                    Debug.WriteLine("Request took: " + watch.ElapsedMilliseconds);
                }
            });

            //First version
            //app.Use<DebugMiddleware>(new DebugMiddlewareOptions
            //{
            //    OnIncomingRequest = (ctx) =>
            //    {
            //        var watch = new Stopwatch();
            //        watch.Start();
            //        ctx.Environment["DebugStopwatch"] = watch;
            //    },
            //    OnOutgoingRequest = (ctx) =>
            //    {
            //        var watch = (Stopwatch)ctx.Environment["DebugStopwatch"];
            //        watch.Stop();
            //        Debug.WriteLine("Request took: " + watch.ElapsedMilliseconds);
            //    }
            //});

            app.Use( async (ctx, next) =>
            {
                await ctx.Response.WriteAsync("<html><head></head><body>Hello World :D</body></html>");
            });
        }
    }
}