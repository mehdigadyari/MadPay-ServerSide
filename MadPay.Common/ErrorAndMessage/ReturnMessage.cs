﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MadPay.Common.ErrorAndMessage
{
    public class ReturnMessage
    {
        public bool status { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string message { get; set; }

    }
}
