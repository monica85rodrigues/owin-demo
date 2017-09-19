namespace Pluralsight.Owin.Demo.Middleware
{
    using Microsoft.Owin;
    using System;

    public class DebugMiddlewareOptions
    {
        public Action<IOwinContext> OnIncomingRequest { get; set; }
        public Action<IOwinContext> OnOutgoingRequest { get; set; }

    }
}