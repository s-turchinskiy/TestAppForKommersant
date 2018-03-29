using System;
using System.Data;
using System.IO;
using System.Text;

namespace TestApp
{
    internal class FileWriter : IWriter
    {
        private readonly IDataReader mReader;
        private readonly string mPathToFile;
        private readonly ILogger mLogger;

        public FileWriter(IDataReader reader, string pathToFile, ILogger logger)
        {
            mReader = reader;
            mPathToFile = pathToFile;
            mLogger = logger;
        }
        public virtual void Write()
        {
            using (StreamWriter csv = new StreamWriter(mPathToFile,
                        true,
                        Encoding.GetEncoding(1251)))
            {

                csv.WriteLine("Id; Name; Age; Cars");

                while (mReader.Read())
                {
                    mLogger.Info("Id: {0}, Name: {1}, Age: {2}, Cars: {3}", mReader.GetInt32(0), mReader.GetString(1),
                        mReader.GetInt32(2), mReader.GetString(3));
                    csv.WriteLine(mReader[0] + ";" + mReader[1] + ";" + mReader[2] + ";" + mReader[3]);
                }
            }
        }
    }
}