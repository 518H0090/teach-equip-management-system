using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.Utilities.OptionPattern;

namespace TeachEquipManagement.BLL.Services
{
    public class GraphService : IGraphService
    {
        private readonly AzureAdConfiguration _azureConfiguration;
        private readonly string _tokenEndpoint;

        public GraphService(IOptionsSnapshot<AzureAdConfiguration> azureConfiguration)
        {
            _azureConfiguration = azureConfiguration.Value;
            _tokenEndpoint = $"https://login.microsoftonline.com/{_azureConfiguration.TenantId}/oauth2/token";
        }


        public async Task GetSharePointDataAsync()
        {
            var scopes = new[] { "User.Read" };

            // Multi-tenant apps can use "common",
            // single-tenant apps must use the tenant ID from the Azure portal
            var tenantId = _azureConfiguration.TenantId;

            // Value from app registration
            var clientId = _azureConfiguration.ClientId;

            // using Azure.Identity;
            var options = new UsernamePasswordCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var userName = "HuynhTran@TrungHieu1204.onmicrosoft.com";
            var password = "TrungHieu1204@";

            // https://learn.microsoft.com/dotnet/api/azure.identity.usernamepasswordcredential
            var userNamePasswordCredential = new UsernamePasswordCredential(
                userName, password, tenantId, clientId, options);

            var graphClient = new GraphServiceClient(userNamePasswordCredential, scopes);

            var testSite = await graphClient.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].GetAsync();

            var testDocument = await graphClient.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Drives.GetAsync();

            var siteId = testSite?.Id?.Split(",")[1];

            var documentId = testDocument?.Value?.FirstOrDefault(x => x.Name == "Avatars")?.Id;

            var result = await graphClient.Drives[documentId].Items["root"].Children.GetAsync();

            var list = await graphClient.Sites["trunghieu1204.sharepoint.com:/sites/FamilyTree:"].Lists["test"].Items.GetAsync();
        }
    }
}
