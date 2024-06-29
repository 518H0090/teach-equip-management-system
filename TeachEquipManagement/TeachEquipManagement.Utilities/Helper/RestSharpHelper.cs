using Polly;
using Polly.Retry;
using RestSharp;
using System.Net;

namespace TeachEquipManagement.Utilities.Helper
{
    public sealed class RestSharpHelper
    {
        private readonly RestClient _restClient;
        private readonly AsyncRetryPolicy _policy;
        private readonly int _maxRetryCount = 3;
        private readonly TimeSpan _timeDelayForRetry = TimeSpan.FromSeconds(10);

        public RestSharpHelper(string url)
        {
            _restClient = new RestClient(url);
            _policy = Policy
           .Handle<Exception>()
           .WaitAndRetryAsync(_maxRetryCount, retryAttempt => _timeDelayForRetry);
        }

        public async Task<TEntity?> Execute<TEntity>(RestRequest request) where TEntity : class, new()
        {
            var response = await _policy.ExecuteAsync(async () =>
            {
                var response = await _restClient.ExecuteAsync<TEntity>(request);
                return response;
            });

            return response.Data;
        }

        public async Task<HttpStatusCode> Execute(RestRequest request)
        {
            var responseStatus = await _policy.ExecuteAsync(async () =>
            {
                var respone = await _restClient.ExecuteAsync(request);
                return respone.StatusCode;
            });

            return responseStatus;
        }

    }
}
