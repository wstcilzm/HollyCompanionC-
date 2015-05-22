using System;
using System.Collections.Generic;
using System.Text;
using Windows.Globalization;

namespace HolyServiceDemo.Common
{
    public class Utilities
    {
        public static string LanguageTagToText(string languageTag)
        {
            string languageText = string.Empty;
            switch (languageTag)
            {
                case "en-US":
                    languageText = "English";
                    break;
                case "zh-Hans-CN":
                    languageText = "中文";
                    break;
                default:                   
                    break;
            }
            return languageText;
        }


        public static string TextToLanguageTag(string languageText)
        {
            string languageTag = string.Empty;
            switch(languageText)
            {
                case "English":
                    languageTag = "en-US";
                    break;
                case "中文":
                    languageTag = "zh-Hans-CN";
                    break;
                default:
                    break;
            }
            return languageTag;
        }
    }
}
