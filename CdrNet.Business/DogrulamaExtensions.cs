using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CdrNet.Infrastructer.Logging;

namespace CdrNet.Business
{
    public static class DogrulamaExtensions
    {
        public static void NullOrWhiteSpace(this string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(paramName, $"{paramName} değeri boş, null yada boşluk karakteri olamaz.");

        }
        public static void Zero(this double value, string paramName)
        {
            if (value == 0)
                throw new ArgumentException(paramName, $"{paramName} değeri 0 olamaz.");
        }
        public static void Zero(this ushort value, string paramName)
        {
            if (value == 0)
                throw new ArgumentException(paramName, $"{paramName} değeri 0 olamaz.");
        }

    }
}

