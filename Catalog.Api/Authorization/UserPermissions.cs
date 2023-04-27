namespace Products.Api.Authorization;

[Flags]
public enum UserPermissions : uint
{
    Read = 1,
    Edit = 2,
    Write = 4,
    Delete = 8
}