using System;
using System.IO;
using System.Net;
using TestOIS.Droid.Services;
using TestOIS.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelperAndroid))]
namespace TestOIS.Droid.Services
{
    public class FileHelperAndroid : IFileHelper
    {
        public void DownloadImage(string url)
        {
            try
            {
                var context = Android.App.Application.Context;
                string pathToNewFolder = Path.Combine(context.GetExternalFilesDir(Android.OS.Environment.DataDirectory.AbsolutePath).ToString(), "MyImages");
                CreateDirectoryIfNotExists(pathToNewFolder);
                WebClient webClient = new WebClient();
                string pathToNewFile = Path.Combine(pathToNewFolder,Path.GetFileName(url));
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
                var context = Android.App.Application.Context;
                string pathToNewFolder = Path.Combine(context.GetExternalFilesDir(Android.OS.Environment.DataDirectory.AbsolutePath).ToString(), "MyImages");
                pathToNewFile  = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                
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

