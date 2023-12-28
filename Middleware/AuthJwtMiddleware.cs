using template.Interface;
using LitJWT;
using LitJWT.Algorithms;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace template.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthJwtMiddleware
    {
        private readonly RequestDelegate _next;
        private IConfiguration _configuration;
        JwtDecoder decoder;
        public AuthJwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            decoder = new JwtDecoder(new HS256Algorithm(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JwtKey"))));
        }

        public Task Invoke(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers.Authorization;
            //Console.WriteLine("middleware called");
            //Console.WriteLine(token);

            if (token != null)
            {
                if (token.Contains("Bearer"))
                {
                    token = token.Replace("Bearer", "").Trim();
                }
                var result = decoder.TryDecode<IJwtUser>(token, out var payload);
                //Console.WriteLine(result);

                if (result == DecodeResult.Success)
                {
                    httpContext.Items.Add("User", payload);
                    Console.WriteLine((payload.Name, payload.Family));
                }

            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthJwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthJwtMiddleware>();
        }
    }
}
