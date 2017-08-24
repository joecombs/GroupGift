using GroupGift.Droid;
using GroupGift.Helpers;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalFileHelper))]
namespace GroupGift.Droid
{
    public class LocalFileHelper : ILocalFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libfolder = Path.Combine(docFolder, "..", "Library", "Databases");
            if (!Directory.Exists(libfolder))
            {
                Directory.CreateDirectory(libfolder);
            }
            return Path.Combine(libfolder, filename);
        }
    }
}
