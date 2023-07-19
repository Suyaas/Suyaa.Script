using Suyaa.Msil.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Lang.Values
{
    /// <summary>
    /// Su字符串
    /// </summary>
    public class SuStringValue : SuValue<string>
    {
        // Su字符串
        public SuStringValue(string value) : base(value)
        {
        }
    }
}
