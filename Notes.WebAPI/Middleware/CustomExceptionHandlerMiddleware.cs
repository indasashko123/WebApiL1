﻿using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using System.Text.Json;
using Notes.Application.Common.Exceptions;

namespace Notes.WebAPI.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke (HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }
        private Task HandleExceptionAsync ( HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch(exception)
            {
                case ValidationException validationException:
                    {
                        code = HttpStatusCode.BadRequest;
                        result = JsonSerializer.Serialize(validationException.Errors);
                        break;
                    }
                case NotFoundException notFoundException:
                    {
                        code = HttpStatusCode.NotFound;
                        break;
                    }
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
}
