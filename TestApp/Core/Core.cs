using System.Data;
using System.IO;

namespace TestApp
{
    internal sealed class Core
    {
        public Core(string pathToFile, int maxAge, ILogger logger)
        {
            if (!PathHelperMethods.IsValidPath(pathToFile))
            {
                logger.Error("Output file path is not valid \"{0}\"", pathToFile);
                return;
            }

            string dirPath = Path.GetDirectoryName(pathToFile);
            if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
            {
                logger.Error("Can't find directory \"{0}\"", dirPath);
                return;
            }
            try
            {
                if (File.Exists(pathToFile))
                {
                    File.Delete(pathToFile);
                }
                using (IDbRequest dbRequest = new DbRequest(maxAge))
                {
                    IDataReader reader = dbRequest.OpenConnectionAndRequestData();
                    IWriter writer = new FileWriter(reader, pathToFile, logger);
                    writer.Write();
                }
            }
            catch (IOException ex)
            {
                logger.Exception("An error has occured while writing to file.", ex);
            }
            catch (System.Exception ex)
            {
                logger.Exception("An error has occured.", ex);
            }
        }
    }
}