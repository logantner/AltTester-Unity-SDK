
namespace Altom.AltUnityDriver.Tests
{
    public class TestsHelper
    {
        public static int GetAltUnityDriverPort()
        {
            string port = System.Environment.GetEnvironmentVariable("ALTUNITYDRIVER_PORT");
            if (!string.IsNullOrEmpty(port))
            {
                return int.Parse(port);
            }

            return 13000;
        }

        public static string GetAltUnityDriverHost()
        {
            string host = System.Environment.GetEnvironmentVariable("ALTUNITYDRIVER_HOST");
            if (!string.IsNullOrEmpty(host))
            {
                return host;
            }

            return "127.0.0.1";
        }

        public static bool FastApproximately(float a, float b, float threshold)
        {
            return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
        }
    }
}
