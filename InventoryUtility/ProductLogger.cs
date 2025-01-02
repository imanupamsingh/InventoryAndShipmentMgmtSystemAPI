﻿namespace InventoryUtility
{
    public class ProductLogger
    {
        // Define the log folder path (ensure it's accessible and correct)
        string _logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

        // Log a message to a text file
        public void LogInformation(string strMessage)
        {
            try
            {
                string logFilePath = Path.Combine(_logFolderPath, $"log_{DateTime.Now:yyyyMMdd}.txt");
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {strMessage}";

                // Append log message to the file
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Handle errors if file cannot be written
                Console.WriteLine($"Error writing log: {ex.Message}");
            }
        }

    }
}
