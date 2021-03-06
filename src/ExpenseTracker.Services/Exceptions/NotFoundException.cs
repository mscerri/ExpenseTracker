﻿using System;

namespace ExpenseTracker.Services.Exceptions
{
    public class NotFoundException : ServiceException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}