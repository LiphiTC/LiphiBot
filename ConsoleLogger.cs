using System;
using System.Linq;
using Twitcher;
using Twitcher.Controllers;
using Twitcher.Controllers.JsonHelper;
using Twitcher.Controllers.APIHelper;
using System.Collections.Generic;
using LiphiBot2.Models;
using Microsoft.Extensions.Logging;

namespace LiphiBot2
{
    public class ConsoleLoggerLiphi : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine(state.ToString());
        }
        
    }
}
