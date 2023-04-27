using Microsoft.EntityFrameworkCore;

namespace Products.Api.Data.Extensions;

public static class EntityOperations
{
    public static void UpdateEntity<T>(this DbContext context, T current, T updated) where T : class
    {
        foreach (var property in context.Entry(current).Properties)
        {
            if (property.Metadata.Name == "Id") 
                continue;
            
            var newValue = property.CurrentValue;
            var oldValue = property.OriginalValue;

            if (updated.GetType().GetProperty(property.Metadata.Name)?.GetValue(updated, null) != null)
            {
                newValue = updated.GetType().GetProperty(property.Metadata.Name)?.GetValue(updated, null);

                if (newValue != null && !newValue.Equals(oldValue))
                {
                    property.CurrentValue = newValue;
                }
            }
        }
    }
}