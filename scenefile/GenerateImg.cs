using System.IO;

namespace scenefile
{
    public static class GenerateImg
    {
        public static Stream GenerateFromLocalFile(string path)
        {
            return File.OpenRead(path);
        }
    }
}