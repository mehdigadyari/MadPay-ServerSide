using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadPay.Common.Helpers
{
    public static class Extensions
    {
        public static void AddAppError(this HttpResponse response, string message)
        {
            response.Headers.Add("App-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "App-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
