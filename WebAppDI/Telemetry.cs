using System.Diagnostics;

namespace WebAppDI
{
    public static class Telemetry
    {
        private static readonly ActivitySource Source = new("WebAppDI");

        public static Activity? StartActivity(string name)
        {
            return Source.StartActivity(name, ActivityKind.Server);
        }
    }
}
