# LogMerge
A simple .NET Core (8.0) console application that merges multiple log files into a single file. The application takes a list of input log file paths and an output log file path as parameters, and merges logs from all input files into the output file in chronological order.
## Features
Merge multiple log files into one.
Maintain chronological order of log entries.
Easy to use with command line arguments.
## Sample Logs
See sample log files under CompanyName.LogMerge.ConsoleApp/Data
## Improvements
### Create UI-Based App
ADD GUI will significantly improve the user experience. A UI-based app will allow users to easily select input files and set the output file path using a visual interface rather than command line arguments.

### Add Multi-Threading
Implementing multi-threading can improve the processing time for merging large log files. By parallelizing the read and write operations, the application can leverage multi-core processors to speed up the merging process.

### Output Error Logs
To handle invalid data and errors more gracefully, the application can be improved to output error logs. This feature will help in troubleshooting by logging details about any issues encountered during the merging process, such as malformed log entries or inaccessible files.
