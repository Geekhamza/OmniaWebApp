using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmniaWeb.ExtraTools
{
    public class DoubleC2toDoubleJs
    {
        public static string changeDouble(string x)
        {
            return (x.Replace(',', '.'));
        }
    }
}