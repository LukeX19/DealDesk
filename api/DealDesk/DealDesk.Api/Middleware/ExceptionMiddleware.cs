﻿using DealDesk.DataAccess.Exceptions;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using DealDesk.Api.Models;

namespace DealDesk.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EntityNotFoundException ex)
            {
                await HandleCustomExceptionAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await HandleCustomExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleCustomExceptionAsync(HttpContext context, Exception ex, HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            var response = new ErrorModel
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonResult = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(jsonResult);
        }
    }
}