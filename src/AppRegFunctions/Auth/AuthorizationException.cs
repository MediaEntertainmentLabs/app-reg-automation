
using System;
using System.Net;
using System.Net.Http;

namespace AppRegFunctions.Auth
{
#pragma warning disable RCS1194 // Implement exception constructors.
    public class AuthorizationException : HttpRequestException
#pragma warning restore RCS1194 // Implement exception constructors.
    {
        public AuthorizationException() : base(null, null, HttpStatusCode.Unauthorized)
        {
        }
        public AuthorizationException(string message, Exception inner) : base(message, inner, HttpStatusCode.Unauthorized)
        {
        }
        public AuthorizationException(string message) : base(message, null, statusCode: HttpStatusCode.Unauthorized)
        {
        }
    }
}
