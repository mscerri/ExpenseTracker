using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ExpenseTracker.Api.Validation
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var modelErrors = context.ModelState.SelectMany(ms => ms.Value.Errors.Select(e => new ValidationError
            {
                Name = ms.Key,
                Description = e.ErrorMessage
            })).ToList();

            if (modelErrors.Count == 0)
            {
                context.Result = new BadRequestObjectResult(new[]
                {
                    new ValidationError
                    {
                        Name = "request",
                        Description = "Request was invalid"
                    }
                });

                return;
            }
            
            context.Result = new BadRequestObjectResult(modelErrors);
        }
    }
}