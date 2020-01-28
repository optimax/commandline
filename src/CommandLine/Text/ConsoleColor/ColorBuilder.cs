using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CommandLine.Text.ConsoleColor
{
    abstract class ColorBuilder
    {
        public static ColorBuilder Create() => Default;
        public static ColorBuilder Default = new AnsiColor();
        public abstract string ColorEncode(string text, string color);
        public abstract void DisplayColorHelp(HelpText inputText, TextWriter writer,bool enable);
    }

    internal class AnsiColor : ColorBuilder
    {
        private const string Pattern = @"(\\u001b\[\d+;?1?m)";
        private readonly Colors colors;
        public AnsiColor()
        {
            colors = new Colors();
        }
      
        /// <summary>
        /// Decode encoded color text and display HelpText with color on terminal
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="writer"></param>
        /// <param name="enable"></param>
        internal void DisplayColorHelp(string inputText, TextWriter writer,bool enable)
        {
            //direct support Ansi color code in OS: Linux, OS x and windows 10
            if (enable)
            {
                writer.Write(inputText);
                return;
            }
            var segments = Regex.Split(inputText, Pattern);
            foreach (var segment in segments) Display(segment, writer);
        }
        /// <summary>
        /// Decode HelpText and display with color on terminal
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="writer"></param>
        /// <param name="enable"></param>
        public override void DisplayColorHelp(HelpText inputText, TextWriter writer,bool enable)
        {
            DisplayColorHelp(inputText.ToString(), writer,enable);
        }

        /// <summary>
        /// Encode text with ansi color codes
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public override string ColorEncode(string text, string color)
        {
            if (color == null) return text;
            if (text == null) return "";
            var col = colors.FirstOrDefault(x => x.Name == color);
            var ansiColor = col?.AnsiCode;
            var resetColor = colors.Reset;
            return ansiColor == null ? text : $"{ansiColor}{text}{resetColor}";
        }

        private void Display(string segment, TextWriter writer)
        {

            if (segment == colors.Reset) Console.ResetColor();
            else
            {
                var colorMap = colors.FirstOrDefault(x => x.AnsiCode == segment);
                if (colorMap == null)
                    writer.Write(segment);
                else
                    Console.ForegroundColor = colorMap.ConsoleColor;
            }
        }

    }

}
