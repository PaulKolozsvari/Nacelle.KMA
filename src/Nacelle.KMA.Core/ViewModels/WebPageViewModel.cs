#region Using Directives

using System;
using Nacelle.KMA.Core.Models;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class WebPageViewModel : BaseViewModel<WebDataModel, bool>
    {
        public WebPageViewModel()
        {
            this.HtmlContent = string.Empty;
        }

        #region Fields

        private string _htmlContent;
        private string _title;

        #endregion //Fields

        #region Properties

        public string HtmlContent
        {
            get => this._htmlContent;
            set => this.SetProperty(ref this._htmlContent, value);
        }

        public string Title
        {
            get => _title;
            set => this.SetProperty(ref this._title, value);
        }

        #endregion //Properties

        #region Methods

        public override void Prepare(WebDataModel parameter)
        {
            Title = parameter.Title;
            var style = "* { font-family: 'Open Sans', Fallback, sans-serif; color: #666666 }";
            HtmlContent = $"<html><head><style>{style}</style></head><body>{parameter.HtmlContent}</body></html>";
        }

        #endregion //Methods
    }
}
