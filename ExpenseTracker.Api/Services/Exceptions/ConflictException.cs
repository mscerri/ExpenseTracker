﻿using System;

namespace ExpenseTracker.Api.Services.Exceptions
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