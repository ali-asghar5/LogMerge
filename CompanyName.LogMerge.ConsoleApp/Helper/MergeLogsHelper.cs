using CompanyName.LogMerge.ConsoleApp.Models;

namespace CompanyName.LogMerge.ConsoleApp.Helper
{
    public static class MergeLogsHelper
    {
        public static void MergeLogFiles(string outputLogFilePath, List<string> inputLogFilePaths)
        {
            try
            {
                List<LogEntry> logEntries = new List<LogEntry>();

                foreach (var inputLogFilePath in inputLogFilePaths)
                {
                    using (StreamReader reader = new StreamReader(inputLogFilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var logEntry = ParseLogEntry(line);
                            if (logEntry != null)
                            {
                                logEntries.Add(logEntry.Value);
                            }
                        }
                    }
                }

                logEntries = logEntries.OrderBy(entry => entry.Timestamp).ToList();

                Directory.CreateDirectory(Path.GetDirectoryName(outputLogFilePath));

                using (StreamWriter writer = new StreamWriter(File.Open(outputLogFilePath, FileMode.Append)))
                {
                    foreach (var entry in logEntries)
                    {
                        writer.WriteLine($"{entry.Timestamp:yyyy-MM-dd HH:mm:ss.fff} {entry.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while merging the log files. Error: {ex.Message}");
                throw;
            }

        }

        public static LogEntry? ParseLogEntry(string logLine)
        {
            string[] tokens = logLine.Split(' ');
            if (tokens.Length < 2)
            {
                return null;
            }

            DateTime parsedTime;
            string message = string.Empty;

            // Parsing timestamp if format is 2018-06-29 14:14:46.675.
            if (tokens.Length >= 2 && DateTime.TryParse($"{tokens[0]} {tokens[1]}", out parsedTime))
            {
                message = string.Join(' ', tokens.Skip(2));
            }
            // Parsing timestamp if first part of the string is a valid date or time.
            else if (DateTime.TryParse(tokens[0], out parsedTime))
            {
                message = string.Join(' ', tokens.Skip(1));
            }

            // Validate message
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }

            return new LogEntry { Timestamp = parsedTime, Message = message };
        }
    }
}
