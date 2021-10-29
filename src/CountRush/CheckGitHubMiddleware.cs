using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountRush
{
    public class CheckGitHubMiddleware
    {

        private readonly RequestDelegate _next;

        public CheckGitHubMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // TODO Check the caller domain

            await _next.Invoke(context);

            //if (context.Request.GetTypedHeaders().Referer.ToString().ToLower().Contains("github.com"))
            //{
            //    await _next.Invoke(context);
            //}

             return;
        }
    }
}
