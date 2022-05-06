using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RafeW.TrueLayer.Pokemon.Engine.Entities.Utilities
{
    public class RequestResult<TResponse>
    {
        public bool Success { get; private set; }
        public TResponse Response { get; private set; }
        public Exception Exception { get; private set; }

        public void SetSuccess(TResponse response)
        {
            Success = true;
            Response = response;
            Exception = null;
        }

        public void SetFailure(Exception exception)
        {
            Exception = exception;
            Success = false;
            Response = default; 
        }
    }
}
