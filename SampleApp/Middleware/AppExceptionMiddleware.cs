using Madyan.Data;
using Madyan.Data.Exception;
using Madyan.Repo.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SampleApp.Middleware
{
    public static class MiddlewareExtensions
    {
        

        public static IApplicationBuilder UseAppExceptionMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AppExceptionMiddleware>();
        }
    }
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";

        public HttpStatusCodeException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, Exception inner) : this(statusCode, inner.ToString()) { }

        public HttpStatusCodeException(HttpStatusCode statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }

    }
    public class AppExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private IEntityBaseRepository<ErrorDetails> ErrorDetailsRepository;
        public AppExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
          //  this.ErrorDetailsRepository = _ErrorDetailsRepository;
        }

        public async Task Invoke(HttpContext context, IEntityBaseRepository<ErrorDetails> _ErrorDetailsRepository)
        {
            this.ErrorDetailsRepository = _ErrorDetailsRepository;
            try
            {
                await next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                this.ErrorDetailsRepository.Add(HandleExceptionAsync(context, ex));
                this.ErrorDetailsRepository.Commit();
                
            }
            catch (Exception exceptionObj)
            {
                 
                this.ErrorDetailsRepository.Add(HandleExceptionAsync(context, exceptionObj));
                this.ErrorDetailsRepository.Commit();

            }
        }

        

        private ErrorDetails HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            ErrorDetails result = null;
            context.Response.ContentType = "application/json";
            if (exception is HttpStatusCodeException)
            {
                result = new ErrorDetails() { Message = exception.Message, StatusCode = (int)exception.StatusCode, ErrorDate=DateTime.Now };

                context.Response.StatusCode = (int)exception.StatusCode;
            }
            else
            {
                result = new ErrorDetails() { Message = "Runtime Error", StatusCode = (int)HttpStatusCode.BadRequest, ErrorDate = DateTime.Now };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            

            return result;
        }

        private ErrorDetails HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ErrorDetails result = new ErrorDetails() { Message = exception.Message, StatusCode = (int)HttpStatusCode.InternalServerError, ErrorDate = DateTime.Now };
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return result;
        }
    }
}
