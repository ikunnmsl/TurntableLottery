﻿using System;
using System.Diagnostics;

namespace TurntableLottery.Utils.Extensions
{
    public static class ExceptionExt
    {
        public static string ExceptionToString(this Exception exception)
        {
            return exception.Demystify().ToString();
        }
    }
}