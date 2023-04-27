using System.Net;
using Auth.API;

namespace Products.Api.Exceptions;

public class NotFoundException<T> : DomainException
{
    public NotFoundException() : base(
        ErrorCodes.NotFoundError, (int)HttpStatusCode.NotFound)
    {
        PlaceholderData.Add("EntityName", typeof(T).Name);
    }
}