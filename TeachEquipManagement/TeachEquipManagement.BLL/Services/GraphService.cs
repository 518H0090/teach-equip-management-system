using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.Utilities.OptionPattern;

using Microsoft.Graph.Drives.Item.Items.Item.CreateLink;
using TeachEquipManagement.Utilities;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;
using Microsoft.Graph.Models.Security;
using DriveUpload = Microsoft.Graph.Drives.Item.Items.Item.CreateUploadSession;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TeachEquipManagement.BLL.Services
{
    public class GraphService : IGraphService
    {
        private readonly AzureAdConfiguration _azureConfiguration;
        private readonly GraphServiceClient _graphService;

        public GraphService(IOptionsSnapshot<AzureAdConfiguration> azureConfiguration, GraphServiceClient graphService)
        {
            _azureConfiguration = azureConfiguration.Value;
            _graphService = graphService;
        }

        private async Task<string> GetSiteCollectionId()
        {
            var siteInfo = await _graphService.Sites[$"{ConstantValues.tenantName}:{ConstantValues.siteCollectionRelative}:"].GetAsync();
            var siteId = siteInfo?.Id?.Split(",")[1];

            return siteId ?? string.Empty;
        }

        private async Task<string> GetDocumenLibraryId(string folderName)
        {
            var siteDocuments = await _graphService.Sites[$"{ConstantValues.tenantName}:{ConstantValues.siteCollectionRelative}:"].Drives.GetAsync();

            var documentId = siteDocuments?.Value?.FirstOrDefault(x => x.Name == folderName)?.Id;

            return documentId ?? string.Empty;
        }

        private async Task<DriveItemCollectionResponse?> GetListDocumentItem(string documentId)
        {

            var result = await _graphService.Drives[documentId].Items["root"].Children.GetAsync();

            return result;
        }


        public async Task GetSharePointDataAsync()
        {
            var testSite = await _graphService.Sites[$"{ConstantValues.tenantName}:{ConstantValues.siteCollectionRelative}:"].GetAsync();

            var testDocument = await _graphService.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Drives.GetAsync();

            var listAlo = await _graphService.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Lists.GetAsync();

            var siteId = await GetSiteCollectionId();

            var documentId = await GetDocumenLibraryId(ConstantValues.documentLibraryName);

            var result = await GetListDocumentItem(documentId);

            var resultItem = await _graphService.Drives[documentId].Items["01TFPUVZ3NTJXUGS3PV5H2VCRU3PW2IID3"].GetAsync();

            var requestBody = new CreateLinkPostRequestBody
            {
                Type = "view",
                Scope = "anonymous",
                RetainInheritedPermissions = false,
                Password = "ThisIsMyPrivatePassword",
            };

            var resultTestLink = await _graphService.Drives[documentId].Items["01TFPUVZ3NTJXUGS3PV5H2VCRU3PW2IID3"].CreateLink.PostAsync(requestBody);

            var list = await _graphService.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Lists["test"].Items.GetAsync();
        }

        public async Task<string> SharePointUploadFileAsync(IFormFile file)
        {

            string newFileId = string.Empty;

            string site = await GetSiteCollectionId();

            string libraryId = await GetDocumenLibraryId(ConstantValues.documentLibraryName);

            var documentId = await GetDocumenLibraryId(ConstantValues.documentLibraryName);

            var targetFolder = _graphService.Drives[documentId].Root;

            using (var ms = new MemoryStream()) 
            {
                await file.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var buf2 = new byte[ms.Length];
                ms.Read(buf2, 0, buf2.Length);

                ms.Position = 0;

                await targetFolder
                          .ItemWithPath(file.FileName)
                          .Content
                          .PutAsync(ms);

                var uploadedItem = await targetFolder.ItemWithPath(file.FileName).GetAsync();

                newFileId = uploadedItem.Id;

                ms.Dispose();
            }

            return newFileId;
        }

    }
}
