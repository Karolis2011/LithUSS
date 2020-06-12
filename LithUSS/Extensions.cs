using System;
using System.Collections.Generic;
using System.Text;

namespace LithUSS
{
    internal static class Helper
    {
        internal static int Clamp(int min, int val, int max)
        {
            return Math.Max(min, Math.Min(val, max));
        }
    }
}
