using ApolloAesthetics.Domain.Common;

namespace ApolloAesthetics.Domain.Entities;

public class SiteSetting : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string DataType { get; set; } = "string"; // string, int, bool, json
    public bool IsPublic { get; set; } = false;
}

