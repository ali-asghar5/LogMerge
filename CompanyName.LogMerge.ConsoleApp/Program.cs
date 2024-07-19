using CompanyName.LogMerge.ConsoleApp.Helper;
using Serilog;

namespace CompanyName.LogMerge.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .WriteTo.Console()
               .CreateLogger();

            Log.Logger.Information("Welcome to LogMerge, this program merges log file content from multiple files into one file.\n");
            List<string> inputLogFilePaths = InputHelper.GetValidInputLogFilePaths();
            string outputLogFilePath = InputHelper.GetValidOutputLogFilePath();

            try
            {
                MergeLogsHelper.MergeLogFiles(outputLogFilePath, inputLogFilePaths);
                Log.Logger.Information($"Output: {outputLogFilePath}");
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"An error occurred: {ex.Message}");
            }
        }

        
    }
}
