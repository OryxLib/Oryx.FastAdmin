using Sentry;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Utilities.SentryIO
{
    public class SentryLog
    {
        //install-package Sentry -v 1.0.1-beta
        public static void Log(string msg)
        {
            using (SentrySdk.Init("https://049ef1436ee848b7895fb2e16914dffc@sentry.io/1316595"))
            {
                SentrySdk.CaptureMessage(msg);
            }
        }

        public static void Log(Exception exc)
        {
            using (SentrySdk.Init("https://049ef1436ee848b7895fb2e16914dffc@sentry.io/1316595"))
            {
                SentrySdk.CaptureException(exc);
            }
        }
    }
}
