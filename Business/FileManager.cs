using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Business
{
    public class FileManager : IFileService
    {
        public string FileSaveToServer(IFormFile file, string filePath)
        {
            var ext = file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower();
            string fileName = Guid.NewGuid().ToString();

            var path = filePath + fileName;
            using (var stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public string FileSaveToFtp(IFormFile file)
        {
            var ext = file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower();
            string fileName = Guid.NewGuid().ToString();

            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create("Ftp Adresi yazılır" + fileName);
            ftpWebRequest.Credentials = new NetworkCredential("Kullanıcı Adı", "Şifre");
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
            using (Stream ftpStream = ftpWebRequest.GetRequestStream())
            {
                file.CopyTo(ftpStream);
            }

            return fileName;
        }

        public byte[] FileSaveToDatabase(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var fileBytes = memoryStream.ToArray();
                string fileString = Convert.ToBase64String(fileBytes);
                return fileBytes;
            }
        }
    }
}

