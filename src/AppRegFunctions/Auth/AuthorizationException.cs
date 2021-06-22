
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppRegFunctions.Auth
{
    public class AuthorizationException : HttpResponseException
    {
        public AuthorizationException() : base(HttpStatusCode.Unauthorized)
        {
        }

        public AuthorizationException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public AuthorizationException(HttpResponseMessage response) : base(response)
        {
        }
    }
}
