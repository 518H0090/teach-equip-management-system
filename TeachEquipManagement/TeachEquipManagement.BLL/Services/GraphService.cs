using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.Utilities.OptionPattern;

using Microsoft.Graph.Drives.Item.Items.Item.CreateLink;

namespace TeachEquipManagement.BLL.Services
{
    public class GraphService : IGraphService
    {
        private readonly AzureAdConfiguration _azureConfiguration;

        public GraphService(IOptionsSnapshot<AzureAdConfiguration> azureConfiguration)
        {
            _azureConfiguration = azureConfiguration.Value;
        }

        public async Task GetSharePointDataAsync()
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var tenantId = _azureConfiguration.TenantId;

            var clientId = _azureConfiguration.ClientId;

            var clientSecret = _azureConfiguration.ClientSecret;

            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            var accessToken = await clientSecretCredential.GetTokenAsync(new Azure.Core.TokenRequestContext(scopes) { });

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            var testSite = await graphClient.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].GetAsync();

            var testDocument = await graphClient.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Drives.GetAsync();

            var listAlo = await graphClient.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Lists.GetAsync();

            var siteId = testSite?.Id?.Split(",")[1];

            var documentId = testDocument?.Value?.FirstOrDefault(x => x.Name == "Avatars")?.Id;

            var result = await graphClient.Drives[documentId].Items["root"].Children.GetAsync();

            var resultItem = await graphClient.Drives[documentId].Items["01TFPUVZ3NTJXUGS3PV5H2VCRU3PW2IID3"].GetAsync();

            var requestBody = new CreateLinkPostRequestBody
            {
                Type = "view",
                Scope = "anonymous",
                RetainInheritedPermissions = false,
                Password = "ThisIsMyPrivatePassword",
            };

            var resultTestLink = await graphClient.Drives[documentId].Items["01TFPUVZ3NTJXUGS3PV5H2VCRU3PW2IID3"].CreateLink.PostAsync(requestBody);

            var list = await graphClient.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Lists["test"].Items.GetAsync();
        }
    }
}
