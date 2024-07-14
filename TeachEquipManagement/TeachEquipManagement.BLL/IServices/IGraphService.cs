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

        Task GetSharePointShareLinkAsync();

        Task DeleteSharePointShareLinkAsync();

        //======================================================

        Task<string> UploadDriveItemAsync(IFormFile file);

        Task<bool> DeleteDriveItemAsync(string itemId);

        Task<string> GetItemShareLink(string itemId);
    }
}
