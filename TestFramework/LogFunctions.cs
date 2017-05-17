using System;

namespace TestFramework
{
    /// <summary>
    /// These methods allow writing additional lines to the SpecFlow log file
    /// The class types correspond to CSS that has been added to allow the colors to display in the output
    /// </summary>
    public static class LogFunctions
    {
        /// <summary>
        /// Write a warning to the log file, these will show in orange text
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        public static void WriteWarning(string message)
        {
            message = "<div class=\"warningMessage\">" + message + "</div>";
            Console.WriteLine(message);
        }
        /// <summary>
        /// Write an error to the log file, these will show in red text
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        public static void WriteError(string message)
        {
            message = "<div class=\"errorMessage\">" + message + "</div>";
            Console.WriteLine(message);
        }
        /// <summary>
        /// Write a success or info message to the log file, these will show in green text
        /// </summary>
        /// <param name="message">Message to be displayed</param>
        public static void WriteInfo(string message)
        {
            message = "<div class=\"infoMessage\">" + message + "</div>";
            Console.WriteLine(message);
        }
    }
}
