using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Drives.Item.Items.Item.CreateLink;
using Microsoft.Graph.Models;
using Polly;
using Polly.Retry;
using Serilog;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.Utilities;
using TeachEquipManagement.Utilities.OptionPattern;

namespace TeachEquipManagement.BLL.Services
{
    public class GraphService : IGraphService
    {
        private readonly AzureAdConfiguration _azureConfiguration;
        private readonly GraphServiceClient _graphService;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly ILogger _logger;

        public GraphService(IOptionsSnapshot<AzureAdConfiguration> azureConfiguration, GraphServiceClient graphService,
            ILogger logger)
        {
            _azureConfiguration = azureConfiguration.Value;
            _graphService = graphService;
            _logger = logger;
            _retryPolicy = Policy
                          .Handle<Exception>()
                          .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(5),
                            (exception, timeSpan, retryCount, context) =>
                            {
                                _logger.Error(exception, $"Retry attempt {retryCount} failed due to {exception.Message}.");
                            });
        }

        public async Task<string> UploadDriveItemAsync(IFormFile file)
        {
            string spoFileId = string.Empty;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            spoFileId = await _retryPolicy.ExecuteAsync(async () =>
            {
                string site = await GetSiteCollectionId();

                var documentId = await GetDocumenLibraryId(site, ConstantValues.documentLibraryName);

                var targetFolder = _graphService.Drives[documentId].Root;

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    await targetFolder
                              .ItemWithPath(file.FileName)
                              .Content
                              .PutAsync(stream);

                    var uploadedItem = await targetFolder.ItemWithPath(file.FileName).GetAsync();

                    spoFileId = uploadedItem?.Id;
                }

                return spoFileId;
            });
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            return spoFileId;
        }

        public async Task<bool> DeleteDriveItemAsync(string itemId)
        {
            bool result = true;

            result = await _retryPolicy.ExecuteAsync(async () =>
            {
               var siteId = await GetSiteCollectionId();

               var documentId = await GetDocumenLibraryId(siteId, ConstantValues.documentLibraryName);

               var driveItem = await GetDriveItem(documentId, itemId);

               if (driveItem == null)
               {
                    result = false;
               }

               else
               {
                    await _graphService.Drives[documentId].Items[itemId].DeleteAsync();
               }

               return result;
            });

            return result;
        }

        public async Task<string> GetItemShareLink(string itemId)
        {
            string spoFileUrl = string.Empty;

            spoFileUrl = await _retryPolicy.ExecuteAsync(async () =>
            {
                var siteId = await GetSiteCollectionId();

                var documentId = await GetDocumenLibraryId(siteId, ConstantValues.documentLibraryName);

                var driveItem = await GetDriveItem(documentId, itemId);

                if (driveItem != null)
                {
                    spoFileUrl = await CreateShareLink(documentId, itemId);
                }

                return spoFileUrl;
            });

            return spoFileUrl;
        }

        #region Support Function

        private async Task<string> GetSiteCollectionId()
        {
            var siteInfo = await _graphService.Sites[$"{ConstantValues.tenantName}:{ConstantValues.siteCollectionRelative}:"].GetAsync();
            var siteId = siteInfo?.Id?.Split(",")[1];

            return siteId ?? string.Empty;
        }

        private async Task<string> GetDocumenLibraryId(string siteId, string folderName)
        {
            var siteDocuments = await _graphService.Sites[siteId].Drives.GetAsync();

            var documentId = siteDocuments?.Value?.FirstOrDefault(x => x.Name == folderName)?.Id;

            return documentId ?? string.Empty;
        }

        private async Task<DriveItemCollectionResponse?> GetListDocumentItem(string documentId)
        {

            var result = await _graphService.Drives[documentId].Items["root"].Children.GetAsync();

            return result;
        }

        private async Task<DriveItem?> GetDriveItem(string documentId, string itemId)
        {

            var result = await _graphService.Drives[documentId].Items[itemId].GetAsync();

            return result;
        }

        private async Task<string> CreateShareLink(string documentId, string itemId)
        {
            var requestBody = new CreateLinkPostRequestBody
            {
                Type = "view",
                Scope = "anonymous",
                RetainInheritedPermissions = false
            };

            var resultShareLink = await _graphService.Drives[documentId].Items[itemId].CreateLink.PostAsync(requestBody);

            var sharingLink = resultShareLink.Link.WebUrl ?? string.Empty;

            return sharingLink;
        }

        #endregion

        #region Test SPO

        public async Task GetSharePointDataAsync()
        {
            var testSite = await _graphService.Sites[$"{ConstantValues.tenantName}:{ConstantValues.siteCollectionRelative}:"].GetAsync();

            var testDocument = await _graphService.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Drives.GetAsync();

            var listAlo = await _graphService.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Lists.GetAsync();

            var siteId = await GetSiteCollectionId();

            var documentId = await GetDocumenLibraryId(siteId, ConstantValues.documentLibraryName);

            var result = await GetListDocumentItem(documentId);

            var resultItem = await _graphService.Drives[documentId].Items["01TFPUVZ47X4ET4Y2JAJEZ4CBQUZXE3XQY"].GetAsync();

            var requestBody = new CreateLinkPostRequestBody
            {
                Type = "view",
                Scope = "anonymous",
                RetainInheritedPermissions = false,
                Password = "ThisIsMyPrivatePassword",
            };

            var resultTestLink = await _graphService.Drives[documentId].Items["01TFPUVZ47X4ET4Y2JAJEZ4CBQUZXE3XQY"].CreateLink.PostAsync(requestBody);

            var aa = await CreateShareLink(documentId, "01TFPUVZ47X4ET4Y2JAJEZ4CBQUZXE3XQY");

            var list = await _graphService.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Lists["test"].Items.GetAsync();
        }

        public async Task<string> SharePointUploadFileAsync(IFormFile file)
        {

            string newFileId = string.Empty;

            string site = await GetSiteCollectionId();

            string libraryId = await GetDocumenLibraryId(site, ConstantValues.documentLibraryName);

            var documentId = await GetDocumenLibraryId(site, ConstantValues.documentLibraryName);

            var targetFolder = _graphService.Drives[documentId].Root;

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);

                var Value = await targetFolder
                          .ItemWithPath(file.FileName)
                          .Content
                          .PutAsync(ms);

                var uploadedItem = await targetFolder.ItemWithPath(file.FileName).GetAsync();

                newFileId = uploadedItem.Id;
            }

            return newFileId;
        }

        public async Task GetSharePointShareLinkAsync()
        {
            var siteId = await GetSiteCollectionId();

            var documentId = await GetDocumenLibraryId(siteId, ConstantValues.documentLibraryName);

            var result = await GetListDocumentItem(documentId);

            var resultItem = await _graphService.Drives[documentId].Items["01TFPUVZ47X4ET4Y2JAJEZ4CBQUZXE3XQY"].GetAsync();

            var requestBody = new CreateLinkPostRequestBody
            {
                Type = "view",
                Scope = "anonymous",
                RetainInheritedPermissions = false
            };

            var resultTestLink = await _graphService.Drives[documentId].Items["01TFPUVZ47X4ET4Y2JAJEZ4CBQUZXE3XQY"].CreateLink.PostAsync(requestBody);
        }

        public async Task DeleteSharePointShareLinkAsync()
        {
            var siteId = await GetSiteCollectionId();

            var documentId = await GetDocumenLibraryId(siteId, ConstantValues.documentLibraryName);

            var result = await GetListDocumentItem(documentId);

            var item = await GetDriveItem(documentId, "01TFPUVZ47X4ET4Y2JAJEZ4CBQUZXE3XQY");

            await _graphService.Drives[documentId].Items["01TFPUVZ47X4ET4Y2JAJEZ4CBQUZXE3XQY"].DeleteAsync();
        }



        #endregion
    }
}
