using Microsoft.AspNetCore.Http;
using QuickTalk.Application.DTOs.ApiResponse;
using QuickTalk.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuickTalk.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(ex, context);
            }
        }

        private Task HandleExceptionAsync(Exception exception, HttpContext context)
        {
            string message = exception.Message;
            HttpStatusCode status;

            switch (exception)
            {
                case NotFoundException:
                    status = HttpStatusCode.NotFound;
                    break;
                
                case UnauthorizedAccessException:
                    status = HttpStatusCode.Unauthorized;
                    break;

                case BadRequestException:
                    status = HttpStatusCode.BadRequest;
                    break;

                case ConflictException:
                    status = HttpStatusCode.Conflict;
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new ApiResponse<string>
            {
                IsSuccess = false,
                Message = message
            };

            var payload = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(payload);
        }
    }
}
