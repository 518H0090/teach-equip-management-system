using Azure.Core;
using Azure.Identity;
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
                var documentId = await GetDocumenLibraryId(ConstantValues.documentLibraryName);

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

        public async Task DeleteDriveItemAsync(string itemId)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
               var documentId = await GetDocumenLibraryId(ConstantValues.documentLibraryName);

               var driveItem = await GetDriveItem(documentId, itemId);

               if (driveItem == null)
               {
                    throw new Exception("Not Found Drive Item");
               }

               else
               {
                    await _graphService.Drives[documentId].Items[itemId].DeleteAsync();
               }
            });
        }

        public async Task<string> GetItemShareLink(string itemId)
        {
            string spoFileUrl = string.Empty;

            spoFileUrl = await _retryPolicy.ExecuteAsync(async () =>
            {
                var documentId = await GetDocumenLibraryId(ConstantValues.documentLibraryName);

                var driveItem = await GetDriveItem(documentId, itemId);

                if (driveItem != null)
                {
                    spoFileUrl = await CreateShareLink(documentId, itemId);
                }

                else
                {
                    throw new Exception("Not Found Drive Item");
                }

                return spoFileUrl;
            });

            return spoFileUrl;
        }

        public async Task<string> GetImageUrl(string itemId)
        {
            string spoFileUrl = string.Empty;

            spoFileUrl = await _retryPolicy.ExecuteAsync(async () =>
            {
                var documentId = await GetDocumenLibraryId(ConstantValues.documentLibraryName);

                var driveItem = await GetDriveItem(documentId, itemId);

                if (driveItem != null)
                {
                    spoFileUrl = driveItem!.WebUrl;
                }

                else
                {
                    throw new Exception("Not Found Drive Item");
                }

                return spoFileUrl;
            });

            return spoFileUrl;
        }

        #region Support Function

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

            var driveItemLink = await _graphService.Drives[documentId].Items[itemId].CreateLink.PostAsync(requestBody);

            var sharingLink = driveItemLink.Link.WebUrl ?? string.Empty;

            return sharingLink;
        }

        #endregion

        public async Task<string> GetAccessGraphToken()
        {
            var clientId = _azureConfiguration.ClientId;
            var tenantId = _azureConfiguration.TenantId;
            var clientSecret = _azureConfiguration.ClientSecret;

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(clientSecret))
            {
                throw new InvalidOperationException("AzureAd settings are not configured properly.");
            }

            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            var tokenRequestContext = new TokenRequestContext(new[] { "https://graph.microsoft.com/.default" });
            AccessToken token = await clientSecretCredential.GetTokenAsync(tokenRequestContext);
            return token.Token;
        }
    }
}
