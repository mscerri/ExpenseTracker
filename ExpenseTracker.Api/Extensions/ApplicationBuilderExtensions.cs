using System;
using System.Net;
using ExpenseTracker.Services.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExpenseTracker.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseExpenseTrackerExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger<Startup>();

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    if (exception is BadHttpRequestException badHttpRequestException)
                    {
                        logger.LogWarning(errorFeature.Error, "Bad request registered - delegating to Kestrel to handle.");
                        throw badHttpRequestException;
                    }

                    var problemDetails = new ProblemDetails
                    {
                        Title = "An unexpected error occurred",
                        Status = (int) HttpStatusCode.InternalServerError,
                        Detail = "An unexpected error occurred. The instance value will be helpful to debug the problem",
                        Instance = Guid.NewGuid().ToString()
                    };

                    logger.LogError(errorFeature.Error, "An exception has been caught, Problem Instance Id = {ProblemInstanceId}.", problemDetails.Instance);

                    if (exception is ServiceException serviceEx)
                    {
                        switch (serviceEx)
                        {
                            case ConflictException conflictException:
                                problemDetails.Title = "Resource already exists";
                                problemDetails.Status = (int)HttpStatusCode.Conflict;
                                problemDetails.Detail = conflictException.Message;
                                break;
                            case NotFoundException notFoundException:
                                problemDetails.Title = "Resource not found";
                                problemDetails.Status = (int)HttpStatusCode.NotFound;
                                problemDetails.Detail = notFoundException.Message;
                                break;
                        }
                    }

                    context.Response.ContentType = "application/problem+json";
                    context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
                });
            });
        }
    }
}