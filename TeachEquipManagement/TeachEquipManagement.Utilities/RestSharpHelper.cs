using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.Utilities
{
    public class RestSharpHelper
    {
        private readonly RestClient _restClient;

        public RestSharpHelper(string url)
        {
            _restClient = new RestClient(url);
        }

        public TEntity? Execute<TEntity> (RestRequest request) where TEntity : class, new()
        {
            var response = _restClient.Execute<TEntity>(request);
            return response.Data;
        }

        public HttpStatusCode Execute(RestRequest request)
        {
            var respone = _restClient.Execute(request);
            return respone.StatusCode;
        }

    }
}
