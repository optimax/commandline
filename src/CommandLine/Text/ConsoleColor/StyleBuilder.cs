namespace CommandLine.Text.ConsoleColor
{
    /// <summary>
    /// Exposes standard delegates to provide a mean to customize part of color help screen generation.
    /// This type is consumed by <see cref="CommandLine.Text.HelpText"/>.
    /// </summary>
    public abstract class StyleBuilder
    {
        /// <summary>
        /// Create instance of <see cref="StyleBuilder"/>
        /// </summary>
        /// <param name="styleType"></param>
        public static void Create(StyleType styleType)
        {
            Default = styleType == StyleType.Color
                ? (StyleBuilder) new ColorStyleBuilder()
                : new DefaultStyleBuilder();
        }

        public static StyleBuilder Default { get; set; } = new DefaultStyleBuilder();

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
    }

    /// <summary>
    /// Default StyleBuilder in non color mode.
    /// </summary>
    internal class DefaultStyleBuilder : StyleBuilder
    {

        public override string ErrorsHeadingTextStyle(string text) => text;
        public override string ErrorStyle(string text) => text;
        public override string UsageStyle(string text) => text;
        public override string UsageHeadingTextStyle(string text) => text;
        public override string OptionStyle(string text) => text;
    }


    /// <summary>
    /// Color styleBuilder.
    /// </summary>
    public class ColorStyleBuilder : StyleBuilder
    {
        //public ColorStyleBuilder()
        //{
        //    ColorBuilder.Default = new AnsiColor();
        //}
        private const string ErrorsHeadingTextStyleColor = "red";
        private const string FormatErrorStyleColor = "red";
        private const string UsageHeadingTextStyleColor = "magenta";
        private const string UsageStyleColor = "cyan";
        private const string OptionStyleColor = "yellow";
        public override string ErrorsHeadingTextStyle(string text) => ColorBuilder.Default.ColorEncode(text, ErrorsHeadingTextStyleColor);
        public override string ErrorStyle(string text) => ColorBuilder.Default.ColorEncode(text, FormatErrorStyleColor);
        public override string UsageStyle(string text) => ColorBuilder.Default.ColorEncode(text, UsageStyleColor);
        public override string UsageHeadingTextStyle(string text) => ColorBuilder.Default.ColorEncode(text, UsageHeadingTextStyleColor);
        public override string OptionStyle(string text) => ColorBuilder.Default.ColorEncode(text, OptionStyleColor);
    }

    public enum StyleType
    {
        None,
        Color
    }
}
