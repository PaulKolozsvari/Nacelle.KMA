#region Using Directives

using System;
using System.Text;
using Nacelle.Core.Helpers;
using Nacelle.KMA.Core.Models;

#endregion //Using Directives

namespace Nacelle.KMA.Core.ViewModels
{
    public class FaqDetailViewModel : BaseViewModel<QuestionItem>
    {
        #region Constructors

        public FaqDetailViewModel()
        {
            this.HtmlContent = string.Empty;
        }

        #endregion //Constructors

        #region Fields

        private string _htmlContent;
        private string _title;

        #endregion //Fields

        #region Properties

        public string Title
        {
            get => _title;
            set => this.SetProperty(ref this._title, value);
        }

        public string HtmlContent
        {
            get => this._htmlContent;
            set => this.SetProperty(ref this._htmlContent, value);
        }

        #endregion //Properties

        #region Methods

        public override void Prepare(QuestionItem parameter)
        {
            Title = parameter.Title;
            var body = EncodingHelper.FromBase64String(parameter.Body);

            var sbStyle = new StringBuilder();
            sbStyle.Append("@font-face{font-family:'opensans';src:'https://fonts.googleapis.com/css?family=Open+Sans:400,700,700italic,400italic'}");
            sbStyle.Append("* {background-color: #F9F9F9; font-size: 16px; color: #666666; font-family: opensans, Fallback, sans-serif;}");
            sbStyle.Append(" a{color:#8BC63E}");//Green Links

            HtmlContent = $"<html><head><meta content = 'width=device-width, initial-scale=1.0' name = 'viewport' ><style>{sbStyle.ToString()}</style></head><body>{body}</body></html>";
        }

        //public void ReloadHtmlContent()
        //{
        //    HtmlContent = string.Empty;
        //    HtmlContent = _htmlContent;
        //}

        #endregion //Methods
    }
}


