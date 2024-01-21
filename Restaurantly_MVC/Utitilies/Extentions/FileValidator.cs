using System.Collections;

namespace Restaurantly_MVC.Utitilies.Extentions
{
    public static class FileValidator
    {
        public static bool ValidatorType(this IFormFile file,string type="image/") 
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }

            return false;
        }

        public static bool ValidatorSize(this IFormFile file, int KB)
        {
            if (file.Length<= KB*1024)
            {
                return true;
            }

            return false;
        }

        public  static string CreatePath(string root,string fileName,params string[] folders)
        {
            string path = root;


            for (int i = 0; i < folders.Length; i++)
            {
               path =Path.Combine(path, folders[i]);
                
            }

            path = Path.Combine(path, fileName);




            return path;
        }

        public async static Task<string> CreateFile(this IFormFile file,string root,  params string[] folders)
        {
            string fileExtention=Path.GetExtension(file.FileName);

            string fileName = $"{Guid.NewGuid()}{fileExtention}";

            string path= CreatePath(root, fileName,folders);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);

            }

            return fileName;
                 
        }

        public  static void DeleteFile(this string fileName, string root, params string[] folders)
        {
            string path= CreatePath(root,fileName,folders);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }




    }
}
