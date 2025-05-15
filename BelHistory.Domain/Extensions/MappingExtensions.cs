using BelHistory.Domain.Database.Objects.Base;
using LocalizedStringApiModel = BelHistory.Domain.API.Models.LocalizedString;
using LocalizedObjectApiModel = BelHistory.Domain.API.Models.LocalizedObject;

namespace BelHistory.Domain.Extensions;

internal static class MappingExtensions
{
    public static LocalizedObjectApiModel ToApiModel(this LocalizedObject localizedObject)
    {
        return new LocalizedObjectApiModel(localizedObject.Id.ToString(), localizedObject.Name.ToApiModel());
    }
    
    public static LocalizedStringApiModel ToApiModel(this LocalizedString localizedString)
    {
        return new LocalizedStringApiModel(localizedString.Be, localizedString.Ru);
    }
}