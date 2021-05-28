
using System;
using Altom.AltUnityDriver.Logging;
using NLog;

namespace Altom.AltUnityDriver.Commands
{
    public class AltBaseCommand
    {
        readonly Logger logger = DriverLogManager.Instance.GetCurrentClassLogger();

        protected IDriverCommunication CommHandler;

        public AltBaseCommand(IDriverCommunication commHandler)
        {
            this.CommHandler = commHandler;
        }

        protected void ValidateResponse(string expected, string received, StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            if (!expected.Equals(received, stringComparison))
            {
                throw new AltUnityInvalidServerResponse(expected, received);
            }
        }


        protected static byte[] decompressScreenshot(byte[] screenshotCompressed)
        {
            using (var memoryStreamInput = new System.IO.MemoryStream(screenshotCompressed))
            using (var memoryStreamOutput = new System.IO.MemoryStream())
            {
                using (var gs = new System.IO.Compression.GZipStream(memoryStreamInput, System.IO.Compression.CompressionMode.Decompress))
                {
                    copyTo(gs, memoryStreamOutput);
                }

                return memoryStreamOutput.ToArray();
            }
        }

        private static void copyTo(System.IO.Stream src, System.IO.Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
    }
}