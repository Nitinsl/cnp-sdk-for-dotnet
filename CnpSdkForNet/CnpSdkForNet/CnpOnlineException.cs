﻿using System;

namespace Cnp.Sdk
{
    public class CnpOnlineException : Exception
    {
        public CnpOnlineException(string message) : base(message)
        {
            
        }

        public CnpOnlineException(string message, Exception e) : base(message, e)
        {

        }
    }
}
