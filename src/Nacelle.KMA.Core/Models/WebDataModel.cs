using System;
namespace Nacelle.KMA.Core.Models
{
    public class WebDataModel
    {
        public WebDataModel(string pageTitle, string htmlContent)
        {
            Title = pageTitle;
            HtmlContent = htmlContent;
        }

        public string Title { get; }
        public string HtmlContent { get; }
    }
}
