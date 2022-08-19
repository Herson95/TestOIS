using System;
using System.IO;
using System.Net;
using TestOIS.Interfaces;
using TestOIS.iOS.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

[assembly: Dependency(typeof(FileHelperIOS))]
namespace TestOIS.iOS.Services
{
    public class FileHelperIOS : IFileHelper
    {
        public void DownloadImage(string url)
        {
            try
            {

                string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"Images");
                CreateDirectoryIfNotExists(pathToNewFolder);
                WebClient webClient = new WebClient();
                string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception)
            {

            }
        }

        public string GetLocalFilePath(string url)
        {
            string pathToNewFile = string.Empty;
            try
            {
                string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),"Images");
                pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));

            }
            catch (Exception)
            {

            }

            return pathToNewFile;


        }

        private void CreateDirectoryIfNotExists(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception)
            {

            }

        }
    }
}

