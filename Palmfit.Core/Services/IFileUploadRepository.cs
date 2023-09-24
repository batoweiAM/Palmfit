using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Core.Services
{
    public interface IFileUploadRepository
    {
        Task<string> UploadImageToCloudinaryAndSave(IFormFile file);
    }
}
