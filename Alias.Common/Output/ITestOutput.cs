namespace Alias.Common.Output
{
    public interface ITestOutput
    {
        /// <summary>
        /// Adds a line of text to the output.
        /// 
        /// </summary>
        /// <param name="message">The message</param>
        void WriteLine(string message);

        /// <summary>
        /// Formats a line of text and adds it to the output.
        /// 
        /// </summary>
        /// <param name="format">The message format</param>
        /// <param name="args">The format arguments</param>
        void WriteLine(string format, params object[] args);

        /// <summary>
        /// Formats a line of text and adds it to the output with a screenshot.
        /// 
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="base64Image">base 64 representation of the image</param>
        void WriteLineWithScreenshot(string message, string base64Image);

        /// <summary>
        /// Formats a line of text and adds it to the output with a screenshot.
        /// 
        /// </summary>
        /// <param name="format">The message format</param>
        /// <param name="base64Image">base 64 representation of the image</param>
        /// <param name="args">The format arguments</param>
        void WriteLineWithScreenshot(string format, string base64Image, params object[] args);
    }
}
