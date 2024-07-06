using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Common
{
    public abstract class ApiResponse<TEntity> where TEntity : class
    {
        public TEntity? Data { get; protected set; }
        public string Message { get; protected set; } = string.Empty;
        public int StatusCode { get; protected set; }

        protected ApiResponse(TEntity? data, string message, int statusCode)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }


    }
}
