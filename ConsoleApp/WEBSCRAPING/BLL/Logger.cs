using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WEBSCRAPING.BLL
{
    public class Logger
    {
        
        public void LogError(Exception ex)
        {
            Write(FormatMessage("ERROR -", ex));
        }

        public void LogError(string message)
        {
            Write(FormatMessage(message));
        }

        public void LogError(string message, Exception ex)
        {
            Write(FormatMessage(message, ex));
        }

        public void LogMessage(string message)
        {
            Write(FormatMessage(message));
        }


        private string[] FormatMessage(string message)
        {
            return new[]
            {
                $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss.fff}] - {message}"
            };
        }

        private string[] FormatMessage(string message, Exception ex)
        {
            return new[]
            {
                $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss.fff}] - {message}",
                string.Empty,
                ex.ToString(),
                string.Empty
            };
        }

        private void Write(string[] texts)
        {
            foreach (var text in texts)
            {
                 Console.WriteLine(text);
            }
          
        }
    }
}
