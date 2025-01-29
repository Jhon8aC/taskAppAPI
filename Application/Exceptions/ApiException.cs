using System.Globalization;

namespace Application.Exceptions
{
    // Custom exception class for API-related errors, inherits from base Exception class
    public class ApiException : Exception
    {
        public ApiException() : base() { }
        // Constructor that accepts a message string and passes it to the base class constructor
        public ApiException(string message) : base(message) { }
        // Constructor that accepts a message format string and arguments, formats the message, and passes it to the base class
        public ApiException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
