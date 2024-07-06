using Polly;
using Polly.Retry;
using RestSharp;
using System.Net;
using System.Runtime.InteropServices;

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

        public async Task<TEntity?> ExecuteAsync<TEntity>(RestRequest request, [Optional] CancellationToken token) where TEntity : new()
        {
            var response = await _policy.ExecuteAsync(async () =>
            {
                var response = await _restClient.ExecuteAsync<TEntity>(request, token);
                return response;
            });

            return response.Data;
        }

        public async Task<RestResponse> ExecuteAsync(RestRequest request, [Optional] CancellationToken token)
        {
            RestResponse response = await _policy.ExecuteAsync(async () =>
            {
                var respone = await _restClient.ExecuteAsync(request, token);
                return respone;
            });

            return response;
        }

    }
}
