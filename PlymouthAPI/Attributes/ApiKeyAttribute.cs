using System; // System.Attribute
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;


namespace PlymouthAPI.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {


        //private const string APIKEYNAME = "ApiKey";
        private const string APIAuthorization = "Authorization";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            //{
            //    context.Result = new ContentResult()
            //    {
            //        StatusCode = 401,
            //        Content = "Api Key was not provided"
            //    };
            //    return;
            //}

            if (!context.HttpContext.Request.Headers.TryGetValue(APIAuthorization, out var extractedAuthorization))
            {
                var error = new { Status = "N", Message = "The Authorization was not provided." };
                var errorResult = new ContentResult
                {
                    StatusCode = 401, // Unauthorized
                    Content = JsonConvert.SerializeObject(error),
                    ContentType = "application/json"
                };
                context.Result = errorResult;
                return;
            }


            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();


            //var apiKey = appSettings.GetValue<string>(APIKEYNAME);
            var authorization = appSettings.GetValue<string>(APIAuthorization);


            //if (!apiKey.Equals(extractedApiKey))
            //{
            //    context.Result = new ContentResult()
            //    {
            //        StatusCode = 401,
            //        Content = "Api Key is not valid"
            //    };
            //    return;
            //}

            if (!authorization.Equals(extractedAuthorization))
            {
                var error = new { Status = "N", Message = "The Authorization is not valid." };
                var errorResult = new ContentResult
                {
                    StatusCode = 401, // Unauthorized
                    Content = JsonConvert.SerializeObject(error),
                    ContentType = "application/json"
                };
                context.Result = errorResult;
                return;
            }
            await next();
        }

    }
}
