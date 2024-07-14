using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.IServices
{
    public interface IGraphService
    {
        Task GetSharePointDataAsync();

        Task<string> SharePointUploadFileAsync(IFormFile file);
    }
}
