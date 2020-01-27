using static CommandLine.Text.ConsoleColor.ColorBuilder;
namespace CommandLine.Text.ConsoleColor
{
    /// <summary>
    /// Exposes standard delegates to provide a mean to customize part of color help screen generation.
    /// This type is consumed by <see cref="CommandLine.Text.HelpText"/>.
    /// </summary>
    public abstract class StyleBuilder
    {
        /// <summary>
        /// Manage StyleType color/none
        /// </summary>
        public static StyleType StyleType = StyleType.None;
        /// <summary>
        /// Create instance of <see cref="StyleBuilder"/>,
        /// </summary>
        /// <returns>The <see cref="StyleBuilder"/> instance.</returns>
        public static StyleBuilder Create() => Factory;
        /// <summary>
        /// Factory to allow custom StyleBuilder injection
        /// </summary>
        public static StyleBuilder Factory => StyleType == StyleType.Color
            ? (StyleBuilder) new ColorStyleBuilder()
            : new DefaultStyleBuilder();
        /// <summary>
        /// Gets a delegate that returns Encoded error text in HelpText.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public abstract string ErrorStyle(string text);
        public abstract string ErrorsHeadingTextStyle(string text);

        /// <summary>
        /// Gets a delegate that returns Encoded Usage Example text in HelpText.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public abstract string UsageStyle(string text);
        /// <summary>
        /// Gets a delegate that returns Encoded UsageHeading text in HelpText.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public abstract string UsageHeadingTextStyle(string text);
        /// <summary>
        /// Gets a delegate that returns Encoded option  text in HelpText.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public abstract string OptionStyle(string text);
        /// <summary>
        /// Default StyleBuilder in non color mode.
        /// </summary>
        private class DefaultStyleBuilder : StyleBuilder
        {

            public override string ErrorsHeadingTextStyle(string text) => text;
            public override string ErrorStyle(string text) => text;
            public override string UsageStyle(string text) =>  text;
            public override string UsageHeadingTextStyle(string text) => text;
            public override string OptionStyle(string text) => text;
        }
    }

    /// <summary>
    /// Color styleBuilder.
    /// </summary>
    public class ColorStyleBuilder : StyleBuilder
    {
        public ColorStyleBuilder()
        {
            Default = new AnsiColor();
        }
        private const string ErrorsHeadingTextStyleColor = "red";
        private const string FormatErrorStyleColor = "red";
        private const string UsageHeadingTextStyleColor = "magenta";
        private const string UsageStyleColor = "cyan";
        private const string OptionStyleColor = "yellow";
        public override string ErrorsHeadingTextStyle(string text) => Default.ColorEncode(text, ErrorsHeadingTextStyleColor);
        public override string ErrorStyle(string text) => Default.ColorEncode(text, FormatErrorStyleColor);
        public override string UsageStyle(string text) => Default.ColorEncode(text, UsageStyleColor);
        public override string UsageHeadingTextStyle(string text) => Default.ColorEncode(text, UsageHeadingTextStyleColor);
        public override string OptionStyle(string text) => Default.ColorEncode(text, OptionStyleColor);
    }



    public enum StyleType
    {
        None,
        Color
    }
}
