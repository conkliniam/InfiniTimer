using InfiniTimer.Enums;
using System.Runtime.CompilerServices;

namespace InfiniTimer.Common
{
    public static class ColorHelper
    {
        public const string Primary = "Primary";
        public const string Secondary = "Secondary";
        public const string Tertiary = "Tertiary";

        public static readonly Dictionary<TimerColor, ColorOption> TimerColors = new()
        {
            {
                TimerColor.Black,
                new ColorOption
                {
                    Name = "Black",
                    Dark = Colors.Black,
                    Light = Color.FromArgb("#AAAAAA")
                }
            },
            {
                TimerColor.Brown,
                new ColorOption
                {
                    Name = "Brown",
                    Dark = Color.FromArgb("#5E2C04"),
                    Light = Color.FromArgb("#FEDDB5")
                }
            },
            {
                TimerColor.Blue,
                new ColorOption
                {
                    Name = "Blue",
                    Dark = Color.FromArgb("#00008A"),
                    Light = Color.FromArgb("#8A8AFF")
                }
            },
            {
                TimerColor.Gray,
                new ColorOption
                {
                    Name = "Gray",
                    Dark = Color.FromArgb("#343434"),
                    Light = Color.FromArgb("#BBBBBB")
                }
            },
            {
                TimerColor.Green,
                new ColorOption
                {
                    Name = "Green",
                    Dark = Color.FromArgb("#006D33"),
                    Light = Color.FromArgb("#00FF33")
                }
            },
            {
                TimerColor.Orange,
                new ColorOption
                {
                    Name = "Orange",
                    Dark = Color.FromArgb("#9B5500"),
                    Light = Color.FromArgb("#FF8700")
                }
            },
            {
                TimerColor.Purple,
                new ColorOption
                {
                    Name = "Purple",
                    Dark = Color.FromArgb("#550055"),
                    Light = Color.FromArgb("#FFBBFF")
                }
            },
            {
                TimerColor.Red,
                new ColorOption
                {
                    Name = "Red",
                    Dark = Color.FromArgb("#8B0000"),
                    Light = Color.FromArgb("#FF8888")
                }
            },
            {
                TimerColor.Yellow,
                new ColorOption
                {
                    Name = "Yellow",
                    Dark = Color.FromArgb("#505000"),
                    Light = Color.FromArgb("#FFFF50")
                }
            },
        };

        public static readonly Dictionary<string, Color> ThemeColors = new()
        {
            {
                Primary,
                Color.FromArgb("#0E1F3B")
            },
            {
                Secondary,
                Color.FromArgb("#DFD8F7")
            },
            {
                Tertiary,
                Color.FromArgb("#01535e")
            }
        };
    }

    public struct ColorOption
    {
        public string Name { get; set; }
        public Color Dark { get; set;}
        public Color Light { get; set;}
    }
}
