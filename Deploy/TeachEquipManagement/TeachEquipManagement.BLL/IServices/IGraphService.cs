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
        Task<string> UploadDriveItemAsync(IFormFile file);

        Task DeleteDriveItemAsync(string itemId);

        Task<string> GetItemShareLink(string itemId);

        Task<string> GetImageUrl(string itemId);

        Task<string> GetAccessGraphToken();
    }
}
