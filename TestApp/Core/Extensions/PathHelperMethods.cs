using System.IO;

namespace TestApp
{
    public static class PathHelperMethods
    {
        public static bool IsValidPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            char[] invalidPathChars = Path.GetInvalidPathChars();
            foreach (char invalidPathChar in invalidPathChars)
            {
                if (path.Contains(invalidPathChar.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
