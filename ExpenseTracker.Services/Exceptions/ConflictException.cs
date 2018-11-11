using System;

namespace ExpenseTracker.Services.Exceptions
{
    public class ConflictException : ServiceException
    {
        public ConflictException(string message) : base(message)
        {
        }

        public ConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}