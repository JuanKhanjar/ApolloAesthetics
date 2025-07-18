using ApolloAesthetics.Domain.Common;

namespace ApolloAesthetics.Domain.Entities;

public class Content : BaseEntity
{
    public string Key { get; set; } = string.Empty; // unique identifier
    public string Title { get; set; } = string.Empty;
    public string TitleAr { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string BodyAr { get; set; } = string.Empty;
    public string MetaTitle { get; set; } = string.Empty;
    public string MetaDescription { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty; // page, blog, faq, etc.
    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }
}

