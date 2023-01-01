using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreCMS3.Helpers
{
    public static class TextHelpers
    {
        //A static method that takes in a string and an integer and shortens it.
        //The string is the content to truncate and the integer represents how many characters are allowed before cutting off the string and adding ellipses ( . . . ).
        public static string TruncateString(string str, int num)
        {
             return str.Length <= num ? str : str.Substring(0, num) + "...";
        }
    }
}