using System.Collections;
using System.Collections.Generic;

namespace CommandLine.Text.ConsoleColor
{
    /// <summary>
    /// Collection of colors
    /// </summary>
    internal class Colors : IEnumerable<ColorMap>
    {
        public string Reset { get; } = "\\u001b[0m";
        private readonly List<ColorMap> colorList = new List<ColorMap>
        {
            //bright colors
            new ColorMap {Name = "black", AnsiCode = "\\u001b[30;1m", ConsoleColor = System.ConsoleColor.Black},
            new ColorMap {Name = "red", AnsiCode = "\\u001b[31;1m", ConsoleColor = System.ConsoleColor.Red},
            new ColorMap {Name = "green", AnsiCode = "\\u001b[32;1m", ConsoleColor = System.ConsoleColor.Green},
            new ColorMap {Name = "yellow", AnsiCode = "\\u001b[33;1m", ConsoleColor = System.ConsoleColor.Yellow},
            new ColorMap {Name = "blue", AnsiCode = "\\u001b[34;1m", ConsoleColor = System.ConsoleColor.Blue},
            new ColorMap {Name = "magenta", AnsiCode = "\\u001b[35;1m", ConsoleColor = System.ConsoleColor.Magenta},
            new ColorMap {Name = "cyan", AnsiCode = "\\u001b[36;1m", ConsoleColor = System.ConsoleColor.Cyan},
            new ColorMap {Name = "white", AnsiCode = "\\u001b[37;1m", ConsoleColor = System.ConsoleColor.White},
        };

        public void Add(ColorMap colorMap)
        {
            colorList.Add(colorMap);
        }

        public void AddRange(IEnumerable<ColorMap> colorMaps)
        {
            colorList.AddRange(colorMaps);
        }

        public IEnumerator<ColorMap> GetEnumerator()
        {
            return colorList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)colorList).GetEnumerator();
        }
    }
}

