using System;

namespace IntelliSun.Text
{
    internal interface IFormatTokenDelegate
    {
        string GetString(string token);
    }
}