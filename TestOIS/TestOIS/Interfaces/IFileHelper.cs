using System;
namespace TestOIS.Interfaces
{
    public interface IFileHelper
    {
        void DownloadImage(string url);

        string GetLocalFilePath(string fileName);
    }
}

