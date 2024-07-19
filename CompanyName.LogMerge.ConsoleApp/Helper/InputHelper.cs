namespace CompanyName.LogMerge.ConsoleApp.Helper
{
    public static class InputHelper
    {
        public static List<string> GetValidInputLogFilePaths()
        {
            while (true)
            {
                Console.WriteLine("Please enter comma separated input log file paths.");
                string inputLogFilePaths = Console.ReadLine();

                // Split the input by commas and trim any whitespace
                List<string> filePaths = inputLogFilePaths.Split(',').Select(path => path.Trim()).ToList();

                // Check if all paths are valid and exist
                if (filePaths.All(path => File.Exists(path)))
                {
                    return filePaths;
                }
                else
                {
                    Console.WriteLine("One or more file paths are invalid or do not exist. Please try again.");
                }
            }
        }

        public static string GetValidOutputLogFilePath()
        {
            while (true)
            {
                Console.WriteLine("Please enter output log file path.");
                string outLogFilePath = Console.ReadLine();

                try
                {
                    // If directory doesn't exists, create it.
                    Directory.CreateDirectory(Path.GetDirectoryName(outLogFilePath));

                    // Try to create or open the file to ensure we have write permissions
                    using (FileStream fs = File.Create(outLogFilePath))
                    {
                        return outLogFilePath;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid output log file path. Error: {ex.Message}");
                }
            }
        }
    }
}
