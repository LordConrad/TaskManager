using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TaskManager.Helpers
{
    public static class BootstrapHtml
    {
        public static MvcHtmlString TextBoxGroupFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string title,
            string id,
            string placeholder)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("<div class=\"form-group\">"));
            sb.AppendLine(string.Format("<label for=\"{0}\">{1}</label>", id, title));
            sb.AppendLine(html.TextBoxFor(expression,
                new { @class = "form-control", @id = id, @placeholder = placeholder, @autocomplete = "off" }).ToString());
            sb.AppendLine(html.ValidationMessageFor(expression).ToString());
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString TextAreaGroupFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string title,
            string id,
            string placeholder,
            int height = 180)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("<div class=\"form-group\">"));
            sb.AppendLine(string.Format("<label for=\"{0}\">{1}</label>", id, title));
            sb.AppendLine(html.TextAreaFor(expression,
                new { @class = "form-control", @id = id, @placeholder = placeholder, @style = string.Format("min-height: {0}px !important;", height) }).ToString());
            sb.AppendLine(html.ValidationMessageFor(expression).ToString());
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString PasswordGroupFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string title,
            string id,
            string placeholder)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("<div class=\"form-group\">"));
            sb.AppendLine(string.Format("<label for=\"{0}\">{1}</label>", id, title));
            sb.AppendLine(html.PasswordFor(expression,
                new { @class = "form-control", @id = id, @placeholder = placeholder }).ToString());
            sb.AppendLine(html.ValidationMessageFor(expression).ToString());
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString DropDownGroupFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> items,
            string title,
            string id,
            string groupId,
            bool isSmall,
            bool isVisible)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("<div id=\"{0}\" class={1} {2}>", groupId, isSmall ? "input-group-sm" : "input-group", !isVisible ? " style=\"display: none;\"" : ""));
            sb.AppendLine(string.Format("<label for=\"{0}\">{1}</label>", id, title));
            sb.AppendLine(html.DropDownListFor(expression, items, new { @class = "form-control", @id = id }).ToString());
            sb.AppendLine(html.ValidationMessageFor(expression).ToString());
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }
        
        public static MvcHtmlString DatePickerGroupFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string title,
            string id,
            string placeholder,
            bool isMinDateToday,
            bool isSmall = false)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("<div class=\"form-group\">"));
            sb.AppendLine(string.Format("<label for=\"{0}\">{1}</label>", id, title));
            sb.AppendLine("<div class=\"controls\">");
            sb.AppendLine(string.Format("<div class=\"{0}\">", isSmall ? "input-group-sm" : "input-group"));
            //sb.AppendLine(string.Format("<input id=\"{0}\" type=\"text\" class=\"date-picker form-control\" />", id));
            sb.AppendLine(html.TextBoxFor(expression, new { @type = "text", @class = "date-picker form-control", @id = id, @style = "cursor: pointer; background-color: #FFF" }).ToString());
            sb.AppendLine(string.Format("<label for=\"{0}\" class=\"input-group-addon btn\"><span class=\"glyphicon glyphicon-calendar\"></span></label></div>", id));
            sb.AppendLine(html.ValidationMessageFor(expression).ToString());
            sb.AppendLine("</div></div>");
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine(string.Format("$(\".date-picker\").datepicker({0});", isMinDateToday ? "{minDate: 0}" : string.Empty));
            sb.AppendLine("$('.date-picker').prop('readonly', true);");
            sb.AppendLine("$(\".date-picker\").on(\"change\", function () {");
            sb.AppendLine("var id = $(this).attr(\"id\");");
            sb.AppendLine("var val = $(\"label[for='\" + id + \"']\").text();");
            sb.AppendLine("$(\"#msg\").text(val + \" changed\");});");
            sb.AppendLine("</script>");
            return new MvcHtmlString(sb.ToString());
        }
    }

}