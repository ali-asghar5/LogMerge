using CompanyName.LogMerge.ConsoleApp.Helper;

namespace CompanyName.LogMerge.ConsoleApp.Tests
{
    public class LogMergeTests
    {
        [Fact]
        public void ParseLogEntry_ValidLogLine_ReturnsLogEntry()
        {
            // Arrange
            string logLine = "2018-06-29 14:14:46.675 Hello Refract!";
            var expectedTimestamp = new DateTime(2018, 6, 29, 14, 14, 46, 675);

            // Act
            var logEntry = MergeLogsHelper.ParseLogEntry(logLine);

            // Assert
            Assert.NotNull(logEntry);
            Assert.Equal(expectedTimestamp, logEntry.Value.Timestamp);
            Assert.Equal("Hello Refract!", logEntry.Value.Message);
        }

        [Fact]
        public void ParseLogEntry_ValidDateOnly_ReturnsLogEntry()
        {
            // Arrange
            string logLine = "2018-06-29 Hello Refract!";
            var expectedTimestamp = new DateTime(2018, 6, 29, 0, 0, 0, 0);

            // Act
            var logEntry = MergeLogsHelper.ParseLogEntry(logLine);

            // Assert
            Assert.NotNull(logEntry);
            Assert.Equal(expectedTimestamp, logEntry.Value.Timestamp);
            Assert.Equal("Hello Refract!", logEntry.Value.Message);
        }

        [Fact]
        public void ParseLogEntry_ValidTimeOnly_ReturnsLogEntry()
        {
            // Arrange
            var currentDateTime = DateTime.Now;
            string logLine = $"{currentDateTime.Hour}:{currentDateTime.Minute} Hello Refract!";
            var expectedTimestamp = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, currentDateTime.Minute, 0, 0);

            // Act
            var logEntry = MergeLogsHelper.ParseLogEntry(logLine);

            // Assert
            Assert.NotNull(logEntry);
            Assert.Equal(expectedTimestamp, logEntry.Value.Timestamp);
            Assert.Equal("Hello Refract!", logEntry.Value.Message);
        }

        [Fact]
        public void ParseLogEntry_InvalidLogLine_MissingTimeStamp_ReturnsNull()
        {
            // Arrange
            string logLine = "Invalid log line";

            // Act
            var logEntry = MergeLogsHelper.ParseLogEntry(logLine);

            // Assert
            Assert.Null(logEntry);
        }

        [Fact]
        public void ParseLogEntry_InvalidLogLine_MissingMessage_ReturnsNull()
        {
            // Arrange
            string logLine = "2018-06-29 14:14:46.675";

            // Act
            var logEntry = MergeLogsHelper.ParseLogEntry(logLine);

            // Assert
            Assert.Null(logEntry);
        }

        [Fact]
        public void MergeLogFiles_ValidInputFiles_MergesCorrectly()
        {
            // Arrange
            var logEntries = new List<string>
            {
                "2018-06-29 14:14:46.675 Log entry 1",
                "2018-06-29 14:15:00.123 Log entry 2",
                "2018-06-29 14:14:50.000 Log entry 3"
            };

            var expectedOutput = new List<string>
            {
                "2018-06-29 14:14:46.675 Log entry 1",
                "2018-06-29 14:14:50.000 Log entry 3",
                "2018-06-29 14:15:00.123 Log entry 2"
            };

            var inputFilePaths = new List<string>();
            foreach (var logEntry in logEntries)
            {
                var tempFile = Path.GetTempFileName();
                File.WriteAllText(tempFile, logEntry + Environment.NewLine);
                inputFilePaths.Add(tempFile);
            }

            string outputFilePath = Path.GetTempFileName();

            try
            {
                // Act
                MergeLogsHelper.MergeLogFiles(outputFilePath, inputFilePaths);

                // Assert
                var mergedLogEntries = File.ReadAllLines(outputFilePath).ToList();
                Assert.Equal(expectedOutput, mergedLogEntries);
            }
            finally
            {
                // Clean up temporary files
                foreach (var path in inputFilePaths)
                {
                    File.Delete(path);
                }
                File.Delete(outputFilePath);
            }
        }
    }
}