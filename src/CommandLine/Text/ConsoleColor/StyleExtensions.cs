﻿using CommandLine.Text.ConsoleColor;

namespace CommandLine.Text
{
    public static class StyleExtensions
    {
        public static string ErrorsHeadingTextStyle(this string text) => StyleBuilder.Default.ErrorsHeadingTextStyle(text);

        public static string ErrorStyle(this string text) => StyleBuilder.Default.ErrorStyle(text);

        public static string UsageHeadingTextStyle(this string text) => StyleBuilder.Default.UsageHeadingTextStyle(text);

        public static string UsageStyle(this string text) => StyleBuilder.Default.UsageStyle(text);

        public static string OptionStyle(this string text) => StyleBuilder.Default.OptionStyle(text);

    }
}
