﻿using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Cubo.Core.Domain;
using Newtonsoft.Json;

namespace Cubo.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var exceptionType = exception.GetType();
            var statusCode = HttpStatusCode.InternalServerError;
            var errorCode = "error";

            switch (exception)
            {
                case CuboException e when exceptionType == typeof(CuboException):
                    errorCode = e.ErrorCode;
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    errorCode = "unauthorized";
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case Exception e when exceptionType == typeof(ArgumentException):
                    errorCode = "invalid_parameter";
                    statusCode = HttpStatusCode.BadRequest;
                    break;
            }

            var response = new { message = exception.Message, errorCode };
            var payload = JsonConvert.SerializeObject(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}