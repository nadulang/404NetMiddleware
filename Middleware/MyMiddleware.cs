using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ContactWebAPI.Middleware
{
    public class MyMiddleware 
{

  private readonly RequestDelegate _next;
  public MyMiddleware(RequestDelegate next)
  {
    _next = next;
  }
  public async Task Invoke(HttpContext httpContext)
  {
                await _next(httpContext);

    if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
    {
        var startTime = DateTime.Now;
                var duration = DateTime.Now - startTime;
                Log1.SaveAllLog
                    (httpContext.Response.StatusCode.ToString(),
                    httpContext.Request.Method.ToString(),
                    httpContext.Request.Path.ToString(),
                    httpContext.Request.Host.ToString()
                    );
            }
            else 
            {
            var startTimeSuccess = DateTime.Now;
            var durationSuccess = DateTime.Now - startTimeSuccess;
            Log1.SaveAllLog
                    (httpContext.Response.StatusCode.ToString(),
                    httpContext.Request.Method.ToString(),
                    httpContext.Request.Path.ToString(),
                    httpContext.Request.Host.ToString());
            }
  }
}
// Extension method used to add the middleware to the HTTP request pipeline.
public static class MyMiddlewareExtensions
{
  public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
  {
    return builder.UseMiddleware<MyMiddleware>();
  }

}

public class Log1
    {
        public static void SaveAllLog(string statusCode, string HTTPMethods, string RequestPath, string RequestHost)
        {
            File.AppendAllText(@"/Users/gigaming/ContactWebAPI/app.log",
                $"{DateTime.Now} Started {HTTPMethods} {RequestPath} for {RequestHost} \n");
            File.AppendAllText(@"/Users/gigaming/ContactWebAPI/app.log",
                $"{DateTime.Now} Completed {statusCode} {RequestPath} not found for {RequestPath} \n");
        }
    }

}