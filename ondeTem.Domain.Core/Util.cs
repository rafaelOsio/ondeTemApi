using System.IO;

namespace ondeTem.Domain.Core
{
    public class Util
    {
        private static string imagesPath = "/var/www/html/assets/images/";
    
        public static bool InsertImage(string path, byte[] bytes)
        {
            File.WriteAllBytes(imagesPath + path, bytes);
            return true;
        }

        public static bool RemoveImage(string path)
        {
            if(path != null && !path.Equals(""))
                File.Delete(imagesPath + path);

            return true;
        }
    }
}