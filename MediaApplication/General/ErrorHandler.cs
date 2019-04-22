using DirectShowLib;
using Media.Extensibility.General;
using System;

namespace MediaApplication.General
{
    public class ErrorHandler : IErrorHandler
    {
        public void ShowError(int statusCode, string message)
        {
            if (statusCode < 0)
            {
                Console.WriteLine(message);
                DsError.ThrowExceptionForHR(statusCode);
            }
        }
    }
}
