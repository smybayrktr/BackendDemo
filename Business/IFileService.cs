using System;
using Microsoft.AspNetCore.Http;

namespace Business
{
	public interface IFileService
	{
		string FileSaveToServer(IFormFile file, string filePath);

        string FileSaveToFtp(IFormFile file);

        byte [] FileSaveToDatabase(IFormFile file);
    }
}

