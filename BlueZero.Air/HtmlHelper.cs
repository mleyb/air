using Recaptcha;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using BlueZero.Air;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public static partial class HtmlHelperExtension
    {
        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

        public static HtmlString Analytics(this HtmlHelper htmlHelper)
        {            
            StringBuilder sb = new StringBuilder();

            sb.Append("<script type='text/javascript'>");
            sb.Append("  var _gaq = _gaq || [];");
            sb.Append(" _gaq.push(['_setAccount', '" + BlueZero.Air.Constants.GAUrchin + "']);");
            sb.Append(" _gaq.push(['_setDomainName', '" + BlueZero.Air.Constants.GADomain + "']);");
            sb.Append(" _gaq.push(['_trackPageview']);");
            sb.Append("  (function() {");
            sb.Append("   var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;");
            sb.Append("    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';");
            sb.Append("   var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);");
            sb.Append("  })();");
            sb.Append("</script>");

            return new HtmlString(sb.ToString());
        }

        public static MvcHtmlString Html(this HtmlHelper helper, string html, bool condition)
        {
            if (condition)
                return MvcHtmlString.Create(html);
            else
                return MvcHtmlString.Empty;
        }

        public static MvcHtmlString Html(this HtmlHelper helper, string trueHtml, string falseHtml, bool condition)
        {
            if (condition)
                return MvcHtmlString.Create(trueHtml);
            else
                return MvcHtmlString.Create(falseHtml);
        }


        public static MvcHtmlString Captcha(this HtmlHelper helper)
        {            
	        var captchaControl = new RecaptchaControl
            {
	            ID = "recaptcha",
                Theme = "white",
                PublicKey = "6LcUf9kSAAAAACOcpv_tYOqfttjK_bNxWoJnLwiW",
                PrivateKey = "6LcUf9kSAAAAAEfGX4DTlj7SVwu93s88L1YitIb9"
            };

	        var htmlWriter = new HtmlTextWriter(new StringWriter());

	        captchaControl.RenderControl(htmlWriter);

            return MvcHtmlString.Create(htmlWriter.InnerWriter.ToString());
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();
            
            TypeConverter converter = TypeDescriptor.GetConverter(enumType);

            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = Extensions.DisplayName(value),
                                                    Value = value.ToString(),
                                                    Selected = value.Equals(metadata.Model)
                                                };

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }

            return htmlHelper.DropDownListFor(expression, items);
        }

        public static MvcHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var imgUrl = expression.Compile()(htmlHelper.ViewData.Model);

            return BuildImageTag(imgUrl.ToString(), null);
        }

        public static MvcHtmlString ImageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            var imgUrl = expression.Compile()(htmlHelper.ViewData.Model);

            return BuildImageTag(imgUrl.ToString(), htmlAttributes);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string imgUrl)
        {
            return BuildImageTag(imgUrl, null);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string imgUrl, object htmlAttributes)
        {
            return BuildImageTag(imgUrl, htmlAttributes);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string action, string controller, object routeValues, string imageURL, string hoverImageURL, string alternateText, object linkHtmlAttributes, object imageHtmlAttributes)
        {
            // Create an instance of UrlHelper
            UrlHelper url = new UrlHelper(html.ViewContext.RequestContext);

            // Create image tag builder
            TagBuilder imageBuilder = new TagBuilder("img");

            // Add image attributes
            imageBuilder.MergeAttribute("src", imageURL);
            imageBuilder.MergeAttribute("alt", alternateText);
            imageBuilder.MergeAttribute("title", alternateText);

            //support for hover
            imageBuilder.MergeAttribute("onmouseover", "this.src='" + hoverImageURL + "'");
            imageBuilder.MergeAttribute("onmouseout", "this.src='" + imageURL + "'");
            imageBuilder.MergeAttribute("border", "0");

            imageBuilder.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes));

            // Create link tag builder
            TagBuilder linkBuilder = new TagBuilder("a");

            // Add attributes
            linkBuilder.MergeAttribute("href", url.Action(action, controller, new RouteValueDictionary(routeValues)));
            linkBuilder.InnerHtml = imageBuilder.ToString(TagRenderMode.SelfClosing);
            linkBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAttributes));

            // Render tag
            return MvcHtmlString.Create(linkBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string action, string controller, string imageURL, string alternateText)
        {
            return ImageLink(html, action, controller, null, imageURL, null, alternateText, null, null);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string action, string controller, object routeValues, string imageURL, string alternateText)
        {
            return ImageLink(html, action, controller, routeValues, imageURL, null, alternateText, null, null);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string action, string controller, object routeValues, string imageURL, string hoverImageURL, string alternateText)
        {
            return ImageLink(html, action, controller, routeValues, imageURL, hoverImageURL, alternateText, null, null);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper html, string action, string controller, string imageURL, string hoverImageURL, string alternateText)
        {
            return ImageLink(html, action, controller, null, imageURL, hoverImageURL, alternateText, null, null);
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }

            return realModelType;
        }

        private static MvcHtmlString BuildImageTag(string imgUrl, object htmlAttributes)
        {
            TagBuilder tag = new TagBuilder("img");

            tag.Attributes.Add("src", imgUrl);

            if (htmlAttributes != null)
                tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}