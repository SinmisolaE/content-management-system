using System;
using CMS.Core.Data;

namespace CMS.Core.Interfaces.Services;

public interface IArticleService
{
    Task<bool> CreateArticleAsync(ArticleCreateCommand articleCreateCommand);
    Task<bool> EditArticleAsync(EditArticle editArticle);
    Task<bool> DeleteArticleAsync(DeleteArticle deleteArticle);
    Task<bool> PublishArticleAsync(Guid articleId);
    Task<ICollection<Article>> ViewArticlesAsync();
}
