using System;

namespace CMS.Core.Data;

public class Article
{
    public Guid Id {get; set;}
    public string Title {get; set;}
    public string Body {get; set;}
    public bool IsPublished { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}
