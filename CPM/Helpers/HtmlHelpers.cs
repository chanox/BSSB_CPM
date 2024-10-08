using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPM_Project.Helpers
{
    public static class HtmlHelpers
    {
        public static string IsSelected(this IHtmlHelper html, string? controller = null, string? action = null, string? cssClass = null)
        {
            if (string.IsNullOrEmpty(cssClass))
                cssClass = "active";

            var currentAction = html.ViewContext.RouteData.Values["action"] as string;
            var currentController = html.ViewContext.RouteData.Values["controller"] as string;

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (string.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : string.Empty;
        }

        public static string? PageClass(this IHtmlHelper htmlHelper)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            return currentAction;
        }

    }
}
