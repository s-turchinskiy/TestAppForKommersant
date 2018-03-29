using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();
            string outputFileName = string.Empty;
            if (args.Length > 0)
            {
                outputFileName = args[0];
            }
            int maxAge = 0;
            if (args.Length > 1)
            {
                string maxAgeStr = args[1];
                if (!int.TryParse(maxAgeStr, out maxAge))
                {
                    logger.Info("Can not parse max age parameter");
                    return;
                }
            }

            Core core = new Core(outputFileName, maxAge, logger);
            logger.Info("Process is completed, press any key to continue");
            Console.ReadKey();
        }
    }
}
