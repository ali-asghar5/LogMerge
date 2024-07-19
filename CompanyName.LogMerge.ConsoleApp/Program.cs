using CompanyName.LogMerge.ConsoleApp.Helper;

namespace CompanyName.LogMerge.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to LogMerge, this program merges log file content from multiple files into one file.\n");
            List<string> inputLogFilePaths = InputHelper.GetValidInputLogFilePaths();
            string outputLogFilePath = InputHelper.GetValidOutputLogFilePath();

            try
            {
                MergeLogsHelper.MergeLogFiles(outputLogFilePath, inputLogFilePaths);
                Console.WriteLine("Log files merged successfully.");
                Console.WriteLine($"Output: {outputLogFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        
    }
}
