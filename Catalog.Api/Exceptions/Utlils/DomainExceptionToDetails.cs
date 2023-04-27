using Microsoft.AspNetCore.Mvc;

namespace Products.Api.Exceptions.Utlils;

public static class DomainExceptionToDetails
{
    public static ProblemDetails ToProblemDetails(this DomainException ex)
    {
        var problems = new ProblemDetails
        {
            Title = ex.Message,
            Status = ex.StatusCode,
            Extensions =
            {
                ["placeholderData"] = ex.PlaceholderData,
            }
        };
            
        if(ex is ValidationFailedException val)
            problems.Extensions["errors"] = val.Errors;
        
        return problems;
    }
}