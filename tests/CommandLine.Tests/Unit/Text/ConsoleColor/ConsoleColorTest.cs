using System.IO;
using CommandLine.Tests.Fakes;
using CommandLine.Text;
using CommandLine.Text.ConsoleColor;
using FluentAssertions;
using Xunit;

namespace CommandLine.Tests.Unit.Text.ConsoleColor
{
    public class ConsoleColorTest: IClassFixture<TestsFixture>
    {

        [Fact]
        public void Color_help_screen_with_ansi_color_enabled_should_be_displayed_encoded()
        {
            // Fixture setup
            var help = new StringWriter();
            var sut = new Parser(config =>
            {
                config.StyleType = StyleType.Color;
                config.HelpWriter = help;
                config.EnableAnsiColor = true;
            });

            // Exercise system
            sut.ParseArguments<Simple_Options>(
                new[] { "--help" });
            var result = help.ToString();

            // Verify outcome
            var i = 0;
            var lines = result.ToNotEmptyLines().TrimStringArray();
            lines[i++].Should().Be(HeadingInfo.Default.ToString());
            lines[i++].Should().Be(CopyrightInfo.Default.ToString());
            lines[i++].Should().BeEquivalentTo(@"\u001b[33;1m--stringvalue     \u001b[0m    Define a string value here.");
            lines[i++].Should().BeEquivalentTo(@"\u001b[33;1m-s, --shortandlong\u001b[0m    Example with both short and long name.");
            lines[i++].Should().BeEquivalentTo(@"\u001b[33;1m-i                \u001b[0m    Define a int sequence here.");
            lines[i++].Should().BeEquivalentTo(@"\u001b[33;1m-x                \u001b[0m    Define a boolean or switch value here.");
            lines[i++].Should().BeEquivalentTo(@"\u001b[33;1m--help            \u001b[0m    Display this help screen.");
            lines[i++].Should().BeEquivalentTo(@"\u001b[33;1m--version         \u001b[0m    Display version information.");
            lines[i].Should().BeEquivalentTo(@"\u001b[33;1mvalue pos. 0      \u001b[0m    Define a long value here.");
        }
        [Theory]
        [InlineData("black", @"\u001b[30;1mHelp Screen!\u001b[0m")]
        [InlineData("red", @"\u001b[31;1mHelp Screen!\u001b[0m")]
        [InlineData("green", @"\u001b[32;1mHelp Screen!\u001b[0m")]
        [InlineData("yellow", @"\u001b[33;1mHelp Screen!\u001b[0m")]
        [InlineData("blue", @"\u001b[34;1mHelp Screen!\u001b[0m")]
        [InlineData("magenta", @"\u001b[35;1mHelp Screen!\u001b[0m")]
        [InlineData("cyan", @"\u001b[36;1mHelp Screen!\u001b[0m")]
        [InlineData("white", @"\u001b[37;1mHelp Screen!\u001b[0m")]
        [InlineData("un_known", "Help Screen!")]
        [InlineData(null, "Help Screen!")]
        public void Message_with_Color_encode_test(string color, string expected)
        {
            ColorBuilder.Default.Enable = true;
            var sut = new AnsiColor();
            var msgEncoded = sut.ColorEncode("Help Screen!", color);
            // Verify outcome
            msgEncoded.Should().Be(expected);
        }

        [Fact]
        public void Decode_colored_help_screen_test()
        {
            // Fixture setup
            var help = @"ConsoleApp 1.0.0
Copyright (c) 2020 Limited Global Company

  \u001b[33;1m--stringvalue     \u001b[0m    Define a string value here.
  \u001b[33;1m-s, --shortandlong\u001b[0m    Example with both short and long name.
  \u001b[33;1m-i                \u001b[0m    Define a int sequence here.
  \u001b[33;1m-x                \u001b[0m    Define a boolean or switch value here.
  \u001b[33;1m--help            \u001b[0m    Display this help screen.
  \u001b[33;1m--version         \u001b[0m    Display version information.
  \u001b[33;1mvalue pos. 0      \u001b[0m    Define a long value here.";

            var expected = @"ConsoleApp 1.0.0
Copyright (c) 2020 Limited Global Company

  --stringvalue         Define a string value here.
  -s, --shortandlong    Example with both short and long name.
  -i                    Define a int sequence here.
  -x                    Define a boolean or switch value here.
  --help                Display this help screen.
  --version             Display version information.
  value pos. 0          Define a long value here.";

            // Exercise system
            var sut = new AnsiColor();
            var helpWriter = new StringWriter();
            sut.DisplayColorHelp(help, helpWriter);
           
            // Verify outcome
            helpWriter.ToString().Should().Be(expected);
        }

        [Fact]
        public void Error_messages_with_color_enabled_should_be_encoded()
        {
            var _ = new Parser(config => config.StyleType = StyleType.Color);
            var text = "ERROR(s):";
            var result = text.ErrorStyle();
           
            // Verify outcome
            var expected = @"\u001b[31;1mERROR(s):\u001b[0m";
            result.Should().Be(expected);

        }

        [Fact]
        public void Usage_messages_with_color_enabled_should_be_encoded()
        {
            var _ = new Parser(config => config.StyleType = StyleType.Color);
            var text = "Usage:";
            var result = text.UsageHeadingTextStyle();
          
            // Verify outcome
            var expected = @"\u001b[35;1mUsage:\u001b[0m";
            result.Should().Be(expected);
        }

        [Fact]
        public void Option_names_with_color_enabled_should_be_color_encoded()
        {
            // Fixture setup
            var _=new Parser(config => config.StyleType = StyleType.Color);
            var text = "-s, --shortandlong";
            // Exercise system
            var result = text.OptionStyle();
            // Verify outcome
            var expected = @"\u001b[33;1m-s, --shortandlong\u001b[0m";
            result.Should().Be(expected);
        }

        [Fact]
        public void Help_with_color_disabled_should_not_be_encoded()
        {
            // Fixture setup
            var _ = new Parser(config => config.StyleType = StyleType.None);
            var text = "-s, --shortandlong";
            // Exercise system
            var result = text.OptionStyle();
            // Verify outcome
            var expected = @"-s, --shortandlong";
            result.Should().Be(expected);
        }
    }
}
