
using Auth.API;

namespace Products.Api.Exceptions;

public class AlreadyExistsException : ConflictException
{
    public AlreadyExistsException(string entityName) : base(ErrorCodes.AlreadyExistsError)
    {
        PlaceholderData.Add("EntityName", entityName);
    }
}